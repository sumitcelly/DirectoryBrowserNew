using System.Web.Optimization;

namespace TestProject
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			// Force optimization to be on or off, regardless of web.config setting
			//BundleTable.EnableOptimizations = false;

			// .debug.js, -vsdoc.js and .intellisense.js files 
			// are in BundleTable.Bundles.IgnoreList by default.
			// Clear out the list and add back the ones we want to ignore.
			// Don't add back .debug.js.
			bundles.IgnoreList.Clear();
			bundles.IgnoreList.Ignore("*-vsdoc.js");
			bundles.IgnoreList.Ignore("*intellisense.js");

			bundles.Add(new ScriptBundle("~/bundles/jsapplibs")				
				.IncludeDirectory("~/Scripts/app/", "*.js", searchSubdirectories: false));

			
			// 3rd Party JavaScript files
			bundles.Add(new ScriptBundle("~/bundles/jsextlibs")
				.Include(
					// jQuery plugins					
					"~/Scripts/lib/TrafficCop.js",
					"~/Scripts/lib/infuser.js", // depends on TrafficCop

					// Knockout and its plugins
					"~/Scripts/lib/knockout-3.4.2.js",
					"~/Scripts/lib/knockout.validation.js",
					"~/Scripts/lib/koExternalTemplateEngine.js",
					"~/Scripts/lib/knockout.mapping-latest.js",
					"~/Scripts/lib/amplify.*"
					));

		}
	}
}