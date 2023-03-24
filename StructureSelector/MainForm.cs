using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI.Selection;

namespace StructureSelector
{
	public partial class MainForm : System.Windows.Forms.Form
	{
		public static string checkBoxSelectedName;
		public static bool checkBoxSelectedStatus;
		public static string checkBoxSelectedTag;
		public static Document document;
		public static Selection selection;

		public MainForm( UIApplication uiapp)
		{
			InitializeComponent();
			document = uiapp.ActiveUIDocument.Document;
			selection = uiapp.ActiveUIDocument.Selection;
			AddCheckBoxesToTabs(uiapp);
		}

		void AddCheckBoxesToTabs(UIApplication uiapp)
		{
			Document document = uiapp.ActiveUIDocument.Document;
			int cordx, cordy;

			//Columns
			List<string> columnsGroupedNames = Structures.GetColumnsGroupedNames(document);
			cordx = 13;
			cordy = 13;
			if (columnsGroupedNames.Count > 0)
			{
				foreach (string name in columnsGroupedNames)
				{
					CheckBox checkBox = new CheckBox();
					checkBox.AutoSize = true;
					checkBox.Location = new System.Drawing.Point(cordx, cordy);
					checkBox.Name = name;
					checkBox.TabIndex = 0;
					checkBox.Text = name;
					checkBox.UseVisualStyleBackColor = true;
					checkBox.AutoSize = false;
					checkBox.Appearance = Appearance.Button;
					checkBox.TextAlign = ContentAlignment.MiddleCenter;
					checkBox.Size = new Size(270, 24);				
					checkBox.Tag = "Columns";
					tabColumns.Controls.Add(checkBox);
					cordy += 24;
					checkBox.CheckedChanged += new EventHandler(this.CheckBoxSelected);
				}
			}			

			//Connections
			List<string> connectionsGroupedNames = Structures.GetConnectionsGroupedNames(document);
			cordx = 13;
			cordy = 13;
			if (connectionsGroupedNames.Count > 0)
			{
				foreach (string name in connectionsGroupedNames)
				{
					CheckBox checkBox = new CheckBox();
					checkBox.AutoSize = true;
					checkBox.Location = new System.Drawing.Point(cordx, cordy);
					checkBox.Name = name;
					checkBox.TabIndex = 0;
					checkBox.Text = name;
					checkBox.UseVisualStyleBackColor = true;
					checkBox.AutoSize = false;
					checkBox.Appearance = Appearance.Button;
					checkBox.TextAlign = ContentAlignment.MiddleCenter;
					checkBox.Size = new Size(270, 24);
					checkBox.Tag = "Connections";
					tabConnections.Controls.Add(checkBox);
					cordy += 24;
					checkBox.CheckedChanged += new EventHandler(this.CheckBoxSelected);
				}
			}			

			//Foundations
			List<string> foundationsGroupedNames = Structures.GetFoundationsGroupedNames(document);
			cordx = 13;
			cordy = 13;
			if (foundationsGroupedNames.Count > 0)
			{
				foreach (string name in foundationsGroupedNames)
				{
					CheckBox checkBox = new CheckBox();
					checkBox.AutoSize = true;
					checkBox.Location = new System.Drawing.Point(cordx, cordy);
					checkBox.Name = name;
					checkBox.TabIndex = 0;
					checkBox.Text = name;
					checkBox.UseVisualStyleBackColor = true;
					checkBox.AutoSize = false;
					checkBox.Appearance = Appearance.Button;
					checkBox.TextAlign = ContentAlignment.MiddleCenter;
					checkBox.Size = new Size(270, 24);
					checkBox.Tag = "Foundations";
					tabFoundations.Controls.Add(checkBox);
					cordy += 24;
					checkBox.CheckedChanged += new EventHandler(this.CheckBoxSelected);
				}
			}			

			//Framings
			List<string> framingsGroupedNames = Structures.GetFramingsGroupedNames(document);
			cordx = 13;
			cordy = 13;
			if (framingsGroupedNames.Count > 0)
			{
				foreach (string name in framingsGroupedNames)
				{
					CheckBox checkBox = new CheckBox();
					checkBox.AutoSize = true;
					checkBox.Location = new System.Drawing.Point(cordx, cordy);
					checkBox.Name = name;
					checkBox.TabIndex = 0;
					checkBox.Text = name;
					checkBox.UseVisualStyleBackColor = true;
					checkBox.AutoSize = false;
					checkBox.Appearance = Appearance.Button;
					checkBox.Size = new Size(270, 24);
					checkBox.TextAlign = ContentAlignment.MiddleCenter;					
					checkBox.Tag = "Framings";
					tabFramings.Controls.Add(checkBox);
					cordy += 24;
					checkBox.CheckedChanged += new EventHandler(this.CheckBoxSelected);
				}
			}


			//Stiffeners
			List<string> stiffenersGroupedNames = Structures.GetStiffenersGroupedNames(document);
			cordx = 13;
			cordy = 13;
			if (stiffenersGroupedNames.Count > 0)
			{
				foreach (string name in stiffenersGroupedNames)
				{
					CheckBox checkBox = new CheckBox();
					checkBox.AutoSize = true;
					checkBox.Location = new System.Drawing.Point(cordx, cordy);
					checkBox.Name = name;
					checkBox.TabIndex = 0;
					checkBox.Text = name;
					checkBox.UseVisualStyleBackColor = true;
					checkBox.AutoSize = false;
					checkBox.Appearance = Appearance.Button;
					checkBox.TextAlign = ContentAlignment.MiddleCenter;
					checkBox.Size = new Size(270, 24);
					checkBox.Tag = "Stiffeners";
					tabStiffeners.Controls.Add(checkBox);
					cordy += 24;
					checkBox.CheckedChanged += new EventHandler(this.CheckBoxSelected);
				}
			}			
		}

		public List<ElementId> structures = new List<ElementId>();
		void CheckBoxSelected(object sender, EventArgs e)
		{
			CheckBox checkBox = sender as CheckBox;
			checkBoxSelectedName = checkBox.Name;
			checkBoxSelectedStatus = checkBox.Checked;
			checkBoxSelectedTag = checkBox.Tag.ToString();

			string checkBoxName = MainForm.checkBoxSelectedName;
			bool checkBoxStatus = MainForm.checkBoxSelectedStatus;
			string checkBoxTag = MainForm.checkBoxSelectedTag;

			if (checkBoxStatus == true)
			{
				if (checkBoxTag == "Columns")
				{
					FilteredElementCollector columnsFiltered = Structures.GetColumns(document);
					foreach (var item in columnsFiltered)
					{
						if (item.Name == checkBoxName)
						{
							structures.Add(item.Id);
						}
					}
				}
				else if (checkBoxTag == "Connections")
				{
					FilteredElementCollector connectionsFiltered = Structures.GetConnections(document);
					foreach (var item in connectionsFiltered)
					{
						if (item.Name == checkBoxName)
						{
							structures.Add(item.Id);
						}
					}
				}
				else if (checkBoxTag == "Foundations")
				{
					FilteredElementCollector foundationsFiltered = Structures.GetFoundations(document);
					foreach (var item in foundationsFiltered)
					{
						if (item.Name == checkBoxName)
						{
							structures.Add(item.Id);
						}
					}
				}
				else if (checkBoxTag == "Framings")
				{
					FilteredElementCollector framingsFiltered = Structures.GetFramings(document);
					foreach (var item in framingsFiltered)
					{
						if (item.Name == checkBoxName)
						{
							structures.Add(item.Id);
						}
					}
				}
				else
				{
					FilteredElementCollector stiffenersFiltered = Structures.GetStiffeners(document);
					foreach (var item in stiffenersFiltered)
					{
						if (item.Name == checkBoxName)
						{
							structures.Add(item.Id);
						}
					}
				}

				selection.SetElementIds(structures);
			}
			else
			{
				//structures.Clear();
				if (checkBoxTag == "Columns")
				{
					FilteredElementCollector columnsFiltered = Structures.GetColumns(document);
					foreach (var item in columnsFiltered)
					{
						if (item.Name == checkBoxName)
						{
							structures.Remove(item.Id);
						}
					}
				}
				else if (checkBoxTag == "Connections")
				{
					FilteredElementCollector connectionsFiltered = Structures.GetConnections(document);
					foreach (var item in connectionsFiltered)
					{
						if (item.Name == checkBoxName)
						{
							structures.Remove(item.Id);
						}
					}
				}
				else if (checkBoxTag == "Foundations")
				{
					FilteredElementCollector foundationsFiltered = Structures.GetFoundations(document);
					foreach (var item in foundationsFiltered)
					{
						if (item.Name == checkBoxName)
						{
							structures.Remove(item.Id);
						}
					}
				}
				else if (checkBoxTag == "Framings")
				{
					FilteredElementCollector framingsFiltered = Structures.GetFramings(document);
					foreach (var item in framingsFiltered)
					{
						if (item.Name == checkBoxName)
						{
							structures.Remove(item.Id);
						}
					}
				}
				else
				{
					FilteredElementCollector stiffenersFiltered = Structures.GetStiffeners(document);
					foreach (var item in stiffenersFiltered)
					{
						if (item.Name == checkBoxName)
						{
							structures.Remove(item.Id);
						}
					}
				}
				selection.SetElementIds(structures);
			}
		}
	}
}
