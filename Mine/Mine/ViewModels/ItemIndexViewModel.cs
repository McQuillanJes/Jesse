using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Mine.Models;
using Mine.Views;

namespace Mine.ViewModels
{
    public class ItemIndexViewModel : BaseViewModel
    {
        public ObservableCollection<ItemModel> DataSet { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemIndexViewModel()
        {
            Title = "Items";
            DataSet = new ObservableCollection<ItemModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemCreatePage, ItemModel>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as ItemModel;
                DataSet.Add(newItem);
                await DataStore.CreateAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DataSet.Clear();
                var items = await DataStore.IndexAsync(true);
                foreach (var item in items)
                {
                    DataSet.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Read an item from the datastore
        /// </summary>
        /// <param name="id"> The ID of the record </param>
        /// <returns> Returns a record from ReadAsync </returns>
        public async Task<ItemModel> ReadAsync(string id)
        {
            //check to see if item exists. Return the item if it does.
            var result = await DataStore.ReadAsync(id);

            return result;
        }

        /// <summary>
        /// Deletes a record from both local memory and the datastore
        /// </summary>
        /// <param name="data"> The record to delete </param>
        /// <returns> True if deleted </returns>
        public async Task<bool> DeleteAsync(ItemModel data)
        {
            //check to see if record exists, if not return null
            var record = await ReadAsync(data.Id);
            if (record == null)
            {
                return false;
            }
                

            //Remove the data locally
            DataSet.Remove(data);

            //remove the data from the data store
            var result = await DataStore.DeleteAsync(data.Id);

            return result;
        }
        
    }
}