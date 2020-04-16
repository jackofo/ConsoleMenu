namespace ConsoleMenu
{
	public class MenuElement
	{
		public string text;
		public delegate void Link();
		public Link link;

		public MenuElement(string text)
		{
			this.text = text;
		}

		public MenuElement(string text, Link method)
		{
			this.text = text;
			this.link += method;
		}
	}
}
