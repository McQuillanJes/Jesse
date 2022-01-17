using System;
/// <summary>
/// Items for the characters and monsters to use
/// </summary>

namespace Mine.Models
{
    public class ItemModel
    {
        //The ID for the item
        public string Id { get; set; }
        //The display text for an item
        public string Text { get; set; }
        //The description for an item
        public string Description { get; set; }
        //The Value of the item (i.e. +9 Damage)
        public int Value { get; set; } = 0;
    }
}