using System.Collections.Generic;

namespace SpearOfLonginus.UI
{
    /// <summary>
    /// A manager for handling UI interfaces.
    /// </summary>
    public abstract class UIManager
    {
        /// <summary>
        /// The item pool.
        /// </summary>
        protected Dictionary<string, UIItem> Items;
        /// <summary>
        /// The items to remove next frame.
        /// </summary>
        protected List<string> ItemsToRemove;

        /// <summary>
        /// Initializes a new instance of the <see cref="UIManager"/> class.
        /// </summary>
        public UIManager()
        {
            Items = new Dictionary<string, UIItem>();
            ItemsToRemove = new List<string>();
        }

        /// <summary>
        /// Updates the manager.
        /// </summary>
        /// <param name="deltatime">The time since the last frame.</param>
        public void Update(float deltatime)
        {
            foreach (var item in Items.Values)
            {
                item.Update(deltatime);
            }

            foreach (var item in ItemsToRemove)
            {
                Items.Remove(item);
            }

            ItemsToRemove.Clear();
        }

        /// <summary>
        /// Adds the item to the pool of items.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(UIItem item)
        {
            AddItem(item.ID, item);
        }

        /// <summary>
        /// Adds the item to the pool of items.
        /// </summary>
        /// <param name="key">The item's key.</param>
        /// <param name="item">The item to add.</param>
        public void AddItem(string key, UIItem item)
        {
            Items.Add(key, item);
        }

        /// <summary>
        /// Removes the item from the pool of items.
        /// </summary>
        /// <param name="key">The item's key.</param>
        public void RemoveItem(string key)
        {
            ItemsToRemove.Add(key);
        }

        /// <summary>
        /// Gets the item from the pool of items.
        /// </summary>
        /// <param name="key">The item's key.</param>
        /// <returns></returns>
        public UIItem GetItem(string key)
        {
            if (!Items.ContainsKey(key))
            {
                return null;
            }

            return Items[key];
        }

        /// <summary>
        /// Gets the item as.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetItemAs<T>(string key) where T : class
        {
            return GetItem(key) as T;
        }
    }
}
