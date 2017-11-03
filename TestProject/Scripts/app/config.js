define('config',
    ['ko'],
    function (ko) {

    	var
                 

			homeFolder = 'testFolder',

			title = 'Directory Browser > ',


			hashes = {
				files: '#/files',
				fileforparent: '#/files/folder',
				search:'#/files/search'
			},

			messages = {
            	viewModelActivated: 'viewmodel-activation'
			},

            stateKeys = {
            	lastView: 'state.active-hash'
            },
            storeExpirationMs = (1000 * 60 * 60 * 24), // 1 day


            viewIds = {
            	files: '#files-view'            	
            },

          

            // methods
            //-----------------

            dataserviceInit = function () { },

         
            init = function () {
            	
            	dataserviceInit();
            	
            };

    	init();

    	return {
    		dataserviceInit: dataserviceInit,
    		hashes: hashes,
    		messages: messages,
    		storeExpirationMs: storeExpirationMs,
    		title: title,
    		viewIds: viewIds,
			stateKeys:stateKeys,
			window: window,
			homeFolder: homeFolder
    	};
    });
