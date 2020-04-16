using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleMenu
{
	public class MenuList : IList<MenuElement>
	{
		MenuElement[] items;
		public MenuElement this[int index] { get => items[index]; set => items[index] = value; }
		public int Count { get => items.Length; }

		public bool IsReadOnly => throw new NotImplementedException();

		public MenuList(int size = 0)
		{
			items = new MenuElement[size];
		}

		public void Add(MenuElement item)
		{
			Array.Resize<MenuElement>(ref items, items.Length + 1);
			items[items.Length - 1] = item;
		}

		public void Add(string text, MenuElement.Link method)
		{
			Array.Resize<MenuElement>(ref items, items.Length + 1);
			items[items.Length - 1] = new MenuElement(text, method);
		}

		public void Add(string text)
		{
			Array.Resize<MenuElement>(ref items, items.Length + 1);
			items[items.Length - 1] = new MenuElement(text);
		}

		public int IndexOf(MenuElement item)
		{
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] == item)
				{
					return i;
				}
			}

			return -1;
		}

		public void Insert(int index, MenuElement item)
		{
			for (int i = 0; i < items.Length; i++)
			{
				if (i == index)
				{
					Array.Resize<MenuElement>(ref items, items.Length + 1);

					for (int j = items.Length - 1; j > i; j--)
					{
						items[j] = items[j - 1];
					}

					items[i] = item;
					return;
				}
			}
		}

		public void RemoveAt(int index)
		{
			for (int i = 0; i < items.Length; i++)
			{
				if (i == index)
				{
					for (int j = i; j < items.Length - 1; j++)
					{
						items[j] = items[j + 1];
					}

					Array.Resize<MenuElement>(ref items, items.Length - 1);
				}
			}
		}

		public void Clear()
		{
			for (int i = 0; i < items.Length; i++)
			{
				items[i] = null;
			}
		}

		public bool Contains(MenuElement item)
		{
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] == item)
				{
					return true;
				}
			}

			return false;
		}

		public void CopyTo(MenuElement[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(MenuElement item)
		{
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] == item)
				{
					for (int j = i; j < items.Length - 1; j++)
					{
						items[j] = items[j + 1];
					}

					Array.Resize<MenuElement>(ref items, items.Length - 1);
					return true;
				}
			}

			return false;
		}

		public MenuEnum GetEnumerator()
		{
			return new MenuEnum(items);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator<MenuElement> IEnumerable<MenuElement>.GetEnumerator()
		{
			return ((IList<MenuElement>)items).GetEnumerator();
		}
	}

	public class MenuEnum : IEnumerator
	{
		public MenuElement[] _elements;

		int position = -1;

		public MenuEnum(MenuElement[] list)
		{
			_elements = list;
		}

		public bool MoveNext()
		{
			position++;
			return (position < _elements.Length);
		}

		public void Reset()
		{
			position = -1;
		}

		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		public MenuElement Current
		{
			get
			{
				try
				{
					return _elements[position];
				}
				catch (IndexOutOfRangeException)
				{
					throw new InvalidOperationException();
				}
			}
		}
	}
}