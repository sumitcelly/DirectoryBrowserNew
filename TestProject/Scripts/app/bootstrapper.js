define('bootstrapper',
    ['jquery','ko', 'config', 'binder','vm.files'],
    function ($,ko, config, binder,vmfiles) {
        	
    	run = function () {
    		binder.bind();
    		vmfiles.activate();             
            };
    
    	return {
    		run: run
    	};
    });