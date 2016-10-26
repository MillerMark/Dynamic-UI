using System;
using Contract;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Hosting;
using System.Windows.Threading;
using DynamicUICore;
using DynamicWPF;

namespace DynamicUIDemo
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// TODO: Focus DynamicButtonArray isn't working when clicked. (ShapeChanger)
		// TODO: Numbers in Assembly Line list and assembly line sample.
		// TODO: Create Plugin2 with standard assembly line elements.
		// TODO: Change window title.
		// TODO: Remove Corner Name.
		// TODO: Save/Load streaming of assembly line. Test. Allows a cancel test.
		// TODO: Bold and Gray/Blue Section headers.
		// TODO: Drag/Drop/Delete in Assembly Line list.
		// TODO: Clear TODO elements.
		// TODO: Icon for app.

		// TODO: tyo template for typeof()
		// TODO: [im template for "[ImportMany(typeof(...))]". [i for [Import(typeof())]
		// TODO: ie type shortcut for IEnumerable.
		
		[ImportMany]
		public List<AssemblyLineControl> FactoryControls { get; set; }

		
		ObservableCollection<AssemblyLineControl> assemblyLine = new ObservableCollection<AssemblyLineControl>();
		

		public MainWindow()
		{
			InitializeComponent();
			ImportPlugIns();
			PopulatePartsList();
			lvAssemblyLine.ItemsSource = assemblyLine;
		}

		private void PopulatePartsList()
		{
			foreach (AssemblyLineControl part in FactoryControls)
				lstParts.Items.Add(part);
		}

		private void ImportPlugIns()
		{
			AggregateCatalog catalog = new AggregateCatalog();
			catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
			var directoryCatalog = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PlugIns");
			catalog.Catalogs.Add(new DirectoryCatalog(directoryCatalog, "*.dll"));

			var container = new CompositionContainer(catalog);
			container.SatisfyImportsOnce(this);
		}

		private void lstControllers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			AssemblyLineControl instance = Activator.CreateInstance(lstParts.SelectedItem.GetType()) as AssemblyLineControl;

			if (instance != null)
			{
				assemblyLine.Add(instance);
				ShowPreview();
			}
		}

		void StateChangeHandler(object sender, EventArgs ea)
		{
			ShowPreview();
		}

		void ShowPreview()
		{
			AssemblyLinePreview.Show(cvsSample, assemblyLine);
		}

		private void lvAssemblyLine_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			DynamicEngine.ShowDynamicUI(spDynamicUI, lvAssemblyLine.SelectedItem, StateChangeHandler);
		}
	}
}
