using System;

namespace ConsoleMenu
{
	public class Menu
	{
		public MenuList elements = new MenuList();
		string continueMessage = "Press any key to continue...";
		string emptyLinkMessage = "Functionality not implemented";
		public enum Skin { TextColor, BackgroundColor };
		Skin skin;
		ConsoleColor defaultForegroundColor = Console.ForegroundColor;
		ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
		MenuElement activeElement;
		bool running = true;

		public void Start()
		{
			Console.CursorVisible = false;
			elements.Add("Exit", () => { running = false; });
			FPSLoop();
		}

		#region draw menu
		void Show()
		{
			switch(skin)
			{
				case Skin.TextColor:
					TextColorSkinDraw();
					break;
				case Skin.BackgroundColor:
					BackgroundColorSkinDraw();
					break;
				default:
					Console.WriteLine("Console menu fatal error :D");
					break;
			}
		}

		void TextColorSkinDraw()
		{
			foreach (var e in elements)
			{
				if (activeElement == e)
				{
					Console.ForegroundColor = ConsoleColor.Red;
				}

				Console.WriteLine(e.text);
				Console.ForegroundColor = defaultForegroundColor;
			}
		}

		void BackgroundColorSkinDraw()
		{
			foreach (var e in elements)
			{
				if (activeElement == e)
				{
					Console.BackgroundColor = ConsoleColor.Red;
				}

				Console.WriteLine(e.text);
				Console.BackgroundColor = defaultBackgroundColor;
			}
		}
		#endregion

		#region main loop
		void Update()
		{
			int temp;
			Show();

			if (activeElement != null)
			{
				temp = elements.IndexOf(activeElement);
			}
			else
			{
				temp = -1;
			}

			ConsoleKey key = Console.ReadKey().Key;

			if (key == ConsoleKey.UpArrow)
			{
				temp--;

				if (temp < 0)
				{
					activeElement = elements[elements.Count - 1];
				}
				else
				{
					activeElement = elements[temp];
				}
			}
			else if (key == ConsoleKey.DownArrow)
			{
				temp++;

				if (temp >= elements.Count)
				{
					activeElement = elements[0];
				}
				else
				{
					activeElement = elements[temp];
				}
			}
			else if (key == ConsoleKey.Enter)
			{
				Console.Clear();

				if (activeElement.link != null)
				{
					activeElement.link();
				}
				else
				{
					Console.WriteLine(emptyLinkMessage);
				}

				if (!running)
					return;

				Console.WriteLine("\n" + continueMessage);
				Console.ReadKey();
				Console.Clear();
			}
		}

		void FPSLoop()
		{
			while (running)
			{
				Console.SetCursorPosition(0, 0);
				Update();
			}
		}
		#endregion

		#region menu settings
		public void SetSkin(Skin skin)
		{
			this.skin = skin;
		}

		public void SetPauseMessage(string text)
		{
			continueMessage = text;
		}

		public void SetEmptyLinkMessage(string text)
		{
			emptyLinkMessage = text;
		}
		#endregion
	}
}