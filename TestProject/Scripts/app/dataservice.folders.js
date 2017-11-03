define('dataservice.folders',
    ['amplify'],
    function (amplify) {
    	var
            init = function () {
            	amplify.request.define('FolderList', 'ajax', {
            		url: '/api/file/{id}',
            		dataType: 'json',
            		type: 'GET'
            	});

            	amplify.request.define('SearchFolders', 'ajax', {
            		url: '/api/search/{id}',
            		dataType: 'json',
            		type: 'GET'
            	});
            	amplify.request.define('DownloadFile', 'ajax', {
            		url: '/api/filetransfer/{id}',
            		dataType: 'binary',
            		type: 'GET'
            	});
            	amplify.request.define('Config', 'ajax', {
            		url: '/api/config',
            		dataType: 'json',
            		type: 'GET'
            	});
            },

            getchildfoldersForParent = function (callbacks, id) {

            	return amplify.request({
            		resourceId: 'FolderList',
            		data: { id: id },
            		success: callbacks.success,
            		error: callbacks.error
            	});
            },

			download = function (callbacks, id) {

				return amplify.request({
					resourceId: 'DownloadFile',
					data: { id: id },
					success: callbacks.success,
					error: callbacks.error
				});
			},

    		search = function (callbacks, id) {

    			return amplify.request({
    				resourceId: 'SearchFolders',
    				data: { id: id },
    				success: callbacks.success,
    				error: callbacks.error
    			});
    		},

			getConfig = function (callback) {
				return amplify.request({
					resourceId: 'Config',					
					success: callback.success,
					error: callback.error
				});
			};

    	init();

    	return {
    		getchildfoldersForParent: getchildfoldersForParent,
    		search: search,
    		download: download,
			getConfig: getConfig
    	};
    });