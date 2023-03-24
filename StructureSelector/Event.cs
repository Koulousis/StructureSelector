using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace StructureSelector
{
	public class Event : IExternalEventHandler
	{
		
		public void Execute(UIApplication uiapp)
		{
			MainForm mainForm = new MainForm(uiapp);
			mainForm.Show();
		}

		public string GetName()
		{
			return "External Event Example";
		}
	}
}
