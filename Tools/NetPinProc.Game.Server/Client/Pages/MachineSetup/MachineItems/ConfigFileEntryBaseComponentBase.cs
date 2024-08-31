using AKSoftware.Localization.MultiLanguages;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using NetPinProc.Domain.MachineConfig;
using System.Net.Http.Json;

namespace NetPinProc.Game.Manager.Client.Pages.Machine_Setup.MachineItems
{
    /// <summary>Base component for CRUD operations on machine items</summary>
    public class ConfigFileEntryBaseComponentBase : ComponentBase
    {
        protected List<string> _events = new();
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected ISnackbar Snackbar { get; set; }
        [Inject] protected IDialogService DialogService { get; set; }
        [Inject] protected ILanguageContainerService lang { get; set; }

        public string ApiUrl { get; set; }

        // events
        protected void StartedEditingItem(ConfigFileEntryBase item)
        {
            _events.Insert(0, $"Event = StartedEditingItem, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
        }

        protected void CanceledEditingItem(ConfigFileEntryBase item)
        {
            _events.Insert(0, $"Event = CanceledEditingItem, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");
        }

        protected async Task CommittedItemChanges(object item)
        {
            _events.Insert(0, $"Event = CommittedItemChanges, Data = {System.Text.Json.JsonSerializer.Serialize(item)}");

            var response = await Http.PutAsJsonAsync(ApiUrl, item);
            if (!response.IsSuccessStatusCode)
            {
                Snackbar.Add("failed to update", Severity.Error);
            }
            else
            {
                var result = await response.Content.ReadFromJsonAsync<object>();
                if (result != null)
                    Snackbar.Add("updated", Severity.Success);
                else
                    Snackbar.Add("failed to update", Severity.Warning);
            }
        }
    }
}
