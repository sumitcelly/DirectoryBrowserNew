define('binder',
    ['jquery', 'ko', 'config', 'vm'],
    function ($, ko, config, vm) {
    	var
            bind = function () {
            	
            	ko.applyBindings(vm.files, $(config.viewIds.files).get(0));            	
            	
            }
    	return {
    		bind: bind
    	};
    });