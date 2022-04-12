using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace DVGB07_viktlund104_Laboration4_Store
{
	public class WebReader
	{
		// read from web 
		private WebClient client;
		private XmlDocument document;

		// store locally after startup
		public BindingList<Book> BookList { get; private set; }
		public BindingList<Game> GameList { get; private set; }
		public BindingList<Movie> MovieList { get; private set; }
		
		
		public static int lastId;
		private List<int> ids;


		public WebReader()
		{
			BookList = new BindingList<Book>();
			GameList = new BindingList<Game>();
			MovieList = new BindingList<Movie>();

			client = new WebClient();
			ids = new List<int>();
		}
		
		public void Load()
		{
			try
			{
				var text = client.DownloadString("https://hex.cse.kau.se/~jonavest/csharp-api");
				document = new XmlDocument();
				document.LoadXml(text);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
			ids.Clear();
			BookList.Clear();
			GameList.Clear();
			MovieList.Clear();
			LoadBooks();
			LoadGames();
			LoadMovies();
			LoadId();
		}
		
		private void LoadBooks()
		{
			var root = document.FirstChild;
			var products = root["products"];
			
			foreach (XmlElement entry in products.ChildNodes)
			{
				if (entry.Name == "book")
				{
					var book = new Book(0);
				
					foreach (XmlElement node in entry.ChildNodes)
					{
						if (node.Name == "stock")
							book.Quantity = int.Parse(node.InnerText);
						if (node.Name == "id")
						{
							book.Id = int.Parse(node.InnerText);
							ids.Add(book.Id);
						}
							
						if (node.Name == "name")
							book.Name = node.InnerText;
						if (node.Name == "price")
							book.Price = double.Parse(node.InnerText);
						if (node.Name == "author")
							book.Author = node.InnerText;
						if (node.Name == "genre")
							book.Genre = node.InnerText;
						if (node.Name == "format")
							book.Format = node.InnerText;
						if (node.Name == "language")
							book.Language = node.InnerText;
					}
					BookList.Add(book);
				}
			}
		}
		
		private void LoadGames()
		{
			var root = document.FirstChild;
			var products = root["products"];

			foreach (XmlElement entry in products.ChildNodes)
			{
				if (entry.Name == "game")
				{
					var game = new Game(0);
				
					foreach (XmlElement node in entry.ChildNodes)
					{
						if (node.Name == "stock")
							game.Quantity = int.Parse(node.InnerText);
						if (node.Name == "id")
						{
							game.Id = int.Parse(node.InnerText);
							ids.Add(game.Id);
						}
						if (node.Name == "name")
							game.Name = node.InnerText;
						if (node.Name == "price")
							game.Price = double.Parse(node.InnerText);
						if (node.Name == "platform")
							game.Platform = node.InnerText;
					}
					GameList.Add(game);
				}
			}
		}
		
		private void LoadMovies()
		{
			var root = document.FirstChild;
			var products = root["products"];

			foreach (XmlElement entry in products.ChildNodes)
			{
				if (entry.Name == "movie")
				{
					var movie = new Movie(0);
				
					foreach (XmlElement node in entry.ChildNodes)
					{
						if (node.Name == "stock")
							movie.Quantity = int.Parse(node.InnerText);
						if (node.Name == "id")
						{
							movie.Id = int.Parse(node.InnerText);
							ids.Add(movie.Id);
						}
						if (node.Name == "name")
							movie.Name = node.InnerText;
						if (node.Name == "price")
							movie.Price = double.Parse(node.InnerText);
						if (node.Name == "format")
							movie.Format = node.InnerText;
						if (node.Name == "playtime")
							movie.PlayingTime = int.Parse(node.InnerText);
					}
					MovieList.Add(movie);
				}
			}
		}
		

		private void LoadId()
		{
			int current = 0;
			
			foreach (var e in ids)
			{
				if (e > current)
				{
					current = e;
				}
			}

			lastId = current;
		}
		
	}
	
	

}