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
        /// Whether or not to hide the UI used for normal gameplay. Used during cutscenes.
        /// </summary>
        public bool HideGameplayUI;

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
        public virtual void Update(float deltatime)
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
        public virtual void AddItem(UIItem item)
        {
            AddItem(item.GetType().Name, item);
        }

        /// <summary>
        /// Adds the item to the pool of items.
        /// </summary>
        /// <param name="key">The item's key.</param>
        /// <param name="item">The item to add.</param>
        public virtual void AddItem(string key, UIItem item)
        {
            item.ID = key;
            Items.Add(key, item);
            SortItems();
        }

        /// <summary>
        /// Removes the item from the pool of items.
        /// </summary>
        /// <param name="key">The item's key.</param>
        public virtual void RemoveItem(string key)
        {
            ItemsToRemove.Add(key);
            SortItems();
        }

        /// <summary>
        /// Gets the item from the pool of items.
        /// </summary>
        /// <param name="key">The item's key.</param>
        /// <returns></returns>
        public virtual UIItem GetItem(string key)
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

        /// <summary>
        /// Sorts the items in the item pool.
        /// </summary>
        protected void SortItems()
        {
            List<UIItem> sorteditems = new List<UIItem>();
            UIItem[] itemarray = new UIItem[Items.Values.Count];
            Items.Values.CopyTo(itemarray, 0);
            sorteditems.AddRange(itemarray);
            sorteditems.Sort();
            Items.Clear();

            foreach (var item in sorteditems)
            {
                Items.Add(item.ID, item);
            }
        }
    }
}
