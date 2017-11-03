(function () {

	var root = this;

	requirejs.config(
        {
        	
        	baseUrl: 'scripts/app' /* script default location */

        	
        }
    );

	// Load the 3rd party libraries
	registerNonAmdLibs();
	// Load our app/custom plug-ins and bootstrap the app
	//loadExtensionsAndBoot();
	boot();
	
	
	function registerNonAmdLibs() {

		define('jquery', [], function () { return root.jQuery; });
		define('ko', [], function () { return root.ko; });
		define('amplify', [], function () { return root.amplify; });
		define('infuser', [], function () { return root.infuser; });
	}
		

	// Load our app/custom plug-ins and bootstrap the app
	function loadExtensionsAndBoot() {		
		requirejs([], boot);
	}

	function boot() {
			// Start-up the app, now that all prerequisites are in place.
		require(['bootstrapper'],
            function (bs) {            	
            	bs.run();
            });
	}

})();