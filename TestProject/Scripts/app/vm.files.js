define('vm.files',
    ['ko', 'config','amplify','dataservice.folders'],
    function (ko,  config,  amplify,dataservice) {

    	var
			parentFolder = ko.observable(),
            files = ko.observableArray(),
    		tmplName = '/views/files.template',
			searchData = ko.observable(),
			homeFolder = '',
			LoadingStatus = ko.observable(),
			searchMode=ko.observable(),
			//drillButtonText = ko.observable()


            activate = function () {
            
            	var routeData = [];
            	
            	if (parentFolder() == '' || parentFolder() == undefined || homeFolder=='') {
            		gethomeFolder().then(function (response) {
            			if (window.location.hash != '') {
            				parentFolder(window.location.hash.substring(1));
            				routeData = window.location.hash.substring(1).split('\\');
            				getFiles(false, JSON.stringify(routeData));
   
            			}
						else
            				getFiles(false, JSON.stringify(response.split('\\')));
            		},
					function (error) {
						console.log('error');
					}
					);
            	}
            	
            },

			browseFurther = function (path) {
				if (path.FileType() == 'File')
				{										
					var filePath = path.FileRelativePath();					
					var fileArray = filePath.split('\\');
					downloadFile(false, JSON.stringify(fileArray));
				}
				else
				{					
					parentFolder(path.FileRelativePath());
					var jsonStr = JSON.stringify(parentFolder().split('\\'));					
					getFiles(false, jsonStr);
					window.location.hash = '#' + parentFolder();
				}
				searchMode(false);
			},
			
			goHome =function()
			{
				searchMode(false);
				if (homeFolder == '') {
					gethomeFolder().then(function (response) {
						console.log("Success!", response);
						getFiles(false, JSON.stringify(response.split('\\')));
					}, function (error) {
						console.error("Failed!", error);
					})
				}
				else {
					console.log(JSON.stringify(homeFolder.split('\\')));
					getFiles(false, JSON.stringify(homeFolder.split('\\')));
					parentFolder(homeFolder);
				}
				window.location.hash = '';
			},

			browseUp = function (path) {
				
				//folders.pop();				
				if (parentFolder() != homeFolder) {
					var folders = parentFolder().split('\\');
					folders.pop();
					var jsonStr = JSON.stringify(folders);
					console.log(jsonStr);
					var tempFolder = "";
					for (i = 0; i < folders.length; i++)
						tempFolder += folders[i] + '\\';
					parentFolder(tempFolder.substring(0, tempFolder.length - 1));
					console.log(tempFolder);
					getFiles(false, jsonStr);
					window.location.hash = '#' + parentFolder();
				}
				else
					alert('At root folder');
			},

			drillButtonText=function(row)
			{
				//console.log(row.FileType());
				if (row.FileType() == 'File')
					return 'Download';
				else
					return 'Drill Further';
			},
		
			

			search = function (inData) {
			
				searchMode(true);
				var tempData = [];				
				var folders = parentFolder().split('\\');
				folders.unshift(searchData());				
				var jsonStr = JSON.stringify(folders);
				console.log(jsonStr);
				searchFiles(false, jsonStr);
			},

			downloadFile = function (forceRefresh, dataToSend) {

				window.open('/api/filetransfer/' + dataToSend);
				
			},

    		uploadFile = function (fileName)
    		{
    			var fd = new FormData();
    			fd.append("fileToUpload", document.getElementById('filechooser').files[0]);

    			$.ajax({
    				url: "api/filetransfer",
    				type: "POST",
    				data: fd,
    				contentType: false,
					processData:false,
    				success: function (response) {
    					alert('Upload Complete');
    				},
    				error: function (jqXHR, textStatus, errorMessage) {
    					console.log(errorMessage); // Optional
    					alert('Error:' + errorMessage);
    				}
    			});
    		},

			searchFiles = function (forceRefresh, dataToSend) {

			 	dataservice.search({
			 		success: function (dtoList) {
			 			files(ko.mapping.fromJS(dtoList)());
			 		},
			 		error: function () {
			 			//alert("Status: " + textStatus); alert("Error: " + errorThrown);
			 		}
			 	},
				dataToSend
            	);

			 },

			gethomeFolder = function ()
			{
				return new Promise
					(function (resolve, reject) {
					dataservice.getConfig({
						success: function (rootFolder) {
							console.log(rootFolder);
							parentFolder(rootFolder);
							homeFolder = parentFolder();
							//alert(parentFolder());
							resolve(homeFolder);
						},
						error: function () {
						}
					});
				});
			}

            getFiles = function (forceRefresh, folder) {
            	//debugger;
            	LoadingStatus('Loading...');
            	dataservice.getchildfoldersForParent({
            		success: function (dtoList) {
            			files(ko.mapping.fromJS(dtoList)());
            			LoadingStatus('Done');
            		},
            		error: function () {
            			//alert("Status: " + textStatus); alert("Error: " + errorThrown);
            		}
            	},
				folder
            	);
            
            },

            init = function () {
            };

    	init();

    	return {
			parentFolder: parentFolder,
    		activate: activate,    		
    		files: files,
			searchData: searchData,
    		tmplName: tmplName,
    		getFiles: getFiles,
    		search: search,
    		browseFurther: browseFurther,
    		browseUp: browseUp,
    		goHome: goHome,
    		uploadFile: uploadFile,
    		drillButtonText: drillButtonText,
    		LoadingStatus: LoadingStatus,
			searchMode: searchMode
    	};
    });