using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine.Models;

namespace Mine.Services
{
    public class MockDataStore : IDataStore<ItemModel>
    {
        readonly List<ItemModel> items;

        public MockDataStore()
        {
            items = new List<ItemModel>()
            {
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Triforce", Description="Power, Wisdom, & Courage.", Value=8 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Mushroom", Description="Makes you bigger", Value=3 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Plasma Sword", Description="It's a sword but it's made of plasma", Value=6 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Varia Suit", Description="Good for hunting metroids", Value=5 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Sneaking Suit", Description="Second only to a cardboard box", Value=7 }
            };
        }

        public async Task<bool> AddItemAsync(ItemModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ItemModel item)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemModel> ReadAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ItemModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}