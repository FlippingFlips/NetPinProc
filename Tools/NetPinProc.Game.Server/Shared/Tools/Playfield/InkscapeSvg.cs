using NetPinProc.Domain.Constants;
using NetPinProc.Domain.MachineConfig;
using NetPinProc.Game.Manager.Shared.Dto;
using Svg;
using System.Drawing;

namespace NetPinProc.Game.Manager.Shared.Tools.Playfield
{
    public class InkscapeSvg
    {
        public static Dictionary<string, IEnumerable<ConfigFileEntryBase>> ExtractPositions(
            string template,
            Dictionary<string, IEnumerable<ConfigFileEntryBase>> machineItems)
        {
            var svg = SvgDocument.FromSvg<SvgDocument>(template);

            //get inkscape namespace
            string inkNs = GetInkNamespace(svg);

            //get all the group layers
            var groups = svg.Children
                .Where(x => x.GetType() == typeof(SvgGroup));

            //get machine items from the layers
            foreach (var group in groups)
            {
                //get label name inkscape namespace
                var groupLayerName = group.CustomAttributes[inkNs + ":label"];

                if (!machineItems.ContainsKey(groupLayerName))
                {
                    Console.WriteLine($"skipping {groupLayerName}, the machine doesn't contain type {groupLayerName}.");
                    continue;
                }

                var mItems = machineItems[groupLayerName].ToDictionary(x => x.Name);

                foreach (var item in group.Children)
                {
                    var name = item.CustomAttributes[inkNs + ":label"];
                    if (mItems.ContainsKey(name))
                    {
                        var mItem = mItems[name];
                        switch (item)
                        {
                            case SvgRectangle r:                           
                                //get the top left point when drawing, center point is saved
                                mItem.XPos = r.X - (r.Width / 2);
                                mItem.YPos = r.Y - (r.Height / 2);
                                Console.WriteLine($"rect:{name}-{groupLayerName} found");
                                break;
                            case SvgCircle c:
                                mItem.XPos = c.CenterX;
                                mItem.YPos = c.CenterY;
                                Console.WriteLine($"circle:{name}-{groupLayerName} found");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"skipping item: {name} in {groupLayerName}, doesn't exist on the machine.");
                    }
                }
            }

            return machineItems;
        }

        private static string GetInkNamespace(SvgDocument svg)
        {
            string inkNs = string.Empty;
            if (svg.Namespaces.ContainsKey("inkscape"))
                inkNs = svg.Namespaces["inkscape"];
            return inkNs;
        }

        /// <summary>This is to generate an initial SVG document from the machine items given<para/>
        /// After generating items for the machine will be in layers with added items from the names coming from machine config.</summary>
        /// <param name="svgDto"></param>
        /// <param name="machineItems"></param>       
        public static Stream GenerateFromTemplate(
            SvgDto svgDto,
            Dictionary<string, IEnumerable<ConfigFileEntryBase>> machineItems)
        {
            //load the file
            var svg = SvgDocument.FromSvg<SvgDocument>(svgDto.Svg);

            //get inkscape namespace
            string inkNs = GetInkNamespace(svg);

            //get all the group layers
            var groups = svg.Children
                .Where(x => x.GetType() == typeof(SvgGroup));            

            //add machine items to the layers
            foreach (var group in groups)
            {
                //get label name inkscape namespace
                var groupLayerName = group.CustomAttributes[inkNs + ":label"];

                //add items for all found in the machine config in the database...
                //positions from the database will be used if set there...
                //the idea here is then to be able to re import an edited SVG to set the positions...
                //...in the database after moving them / editing in inkscape
                if (machineItems.ContainsKey(groupLayerName))
                {
                    //check if we need to not include a collection
                    if(svgDto.Include?.Any() ?? false)
                    {
                        if (svgDto.Include.ContainsKey(groupLayerName) &&
                            !svgDto.Include[groupLayerName])
                            continue;
                    }

                    //either included or doesn't exist, add anyway
                    switch (groupLayerName)
                    {
                        case Names.LEDS:
                        case Names.GI:
                        case Names.LAMPS:
                            foreach (var item in machineItems[groupLayerName])
                            {
                                SvgCircle circle = CreateCircle(inkNs, item, MachineItemColors.COLOR_LEDS);
                                group.Children.Add(circle);
                            }
                            break;
                        case Names.DRIVERS:
                            foreach (var item in machineItems[groupLayerName])
                            {
                                SvgRectangle rect = CreateRect(inkNs, item, MachineItemColors.COLOR_DRIVERS);
                                group.Children.Add(rect);
                            }
                            break;
                        case Names.SWITCHES:
                            foreach (var item in machineItems[groupLayerName])
                            {
                                SvgRectangle rect = CreateRect(inkNs, item, MachineItemColors.COLOR_SWITCHES);
                                group.Children.Add(rect);
                            }
                            break;
                        case Names.STEPPERS:
                            foreach (var item in machineItems[groupLayerName])
                            {
                                SvgRectangle rect = CreateRect(inkNs, item, MachineItemColors.COLOR_STEPPERS);
                                group.Children.Add(rect);
                            }
                            break;
                        case Names.SERVOS:
                            foreach (var item in machineItems[groupLayerName])
                            {
                                SvgRectangle rect = CreateRect(inkNs, item, MachineItemColors.COLOR_SERVOS);
                                group.Children.Add(rect);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            //add an image under the layers? blueprints
            if (!string.IsNullOrWhiteSpace(svgDto.Image))
            {
                //create a new group and image
                var g = new SvgGroup() { ID = "blueprints" };
                var img = new SvgImage
                {
                    ID = "playfieldblueprint",
                    Width = svg.ViewBox.Width,
                    Height = svg.ViewBox.Height,
                    X = 0,
                    Y = 0,
                    AspectRatio = new SvgAspectRatio(SvgPreserveAspectRatio.none),
                    Href = svgDto.Image
                };

                //have to add to the group child and nodes
                g.Children.Add(img);
                g.Nodes.Add(img);

                //have to add to the document child and nodes
                svg.Children.Add(g);
                svg.Nodes.Add(g);

                //do some jiggery pokery to put the image under the layers
                var child = svg.Nodes[0];
                var first = svg.Nodes[svg.Nodes.Count - 1];
                svg.Nodes[0] = first;
                svg.Nodes[svg.Nodes.Count - 1] = child;
            }            

            var ms = new MemoryStream();
            svg.Write(ms);
            return ms;
        }

        /// <summary>Create circle no offset</summary>
        /// <param name="inkNs"></param>
        /// <param name="item"></param>
        /// <param name="colorHtml"></param>
        /// <returns></returns>
        private static SvgCircle CreateCircle(string inkNs, ConfigFileEntryBase item, string colorHtml)
        {
            var circle = new SvgCircle()
            {
                CenterX = 0,
                CenterY = 0,
                Radius = 10,
                Fill = new SvgColourServer() { Colour = ColorTranslator.FromHtml($"#{colorHtml}") },
            };

            if (item.XPos.HasValue)
                circle.CenterX = new SvgUnit(float.Parse(item.XPos.Value.ToString()));

            if (item.YPos.HasValue)
                circle.CenterY = new SvgUnit(float.Parse(item.YPos.Value.ToString()));

            circle.CustomAttributes.Add(inkNs + ":label", item.Name);
            return circle;
        }

        /// <summary>Create rect with offsets</summary>
        /// <param name="inkNs"></param>
        /// <param name="item"></param>
        /// <param name="colorHtml"></param>
        /// <returns></returns>
        private static SvgRectangle CreateRect(string inkNs, ConfigFileEntryBase item, string colorHtml)
        {
            var rect = new SvgRectangle()
            {
                Width = 20,
                Height = 20,
                X = new SvgUnit(0),
                Y = new SvgUnit(0),
                Fill = new SvgColourServer() { Colour = ColorTranslator.FromHtml($"#{colorHtml}") },
            };

            if (item.XPos.HasValue && item.XPos.Value > 1)
            {
                var posX = item.XPos.Value + (rect.Width / 2);
                rect.X = new SvgUnit(float.Parse(posX.ToString()));                
            }                

            if (item.YPos.HasValue)
            {
                var posY = item.YPos.Value + (rect.Width / 2);
                rect.Y = new SvgUnit(float.Parse(posY.ToString()));
            }

            rect.CustomAttributes.Add(inkNs + ":label", item.Name);
            return rect;
        }
    }
}
