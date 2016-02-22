var explorerApp = angular.module('explorerApp',[]);
explorerApp.controller('ExplorerCtrl',  
	function ($scope, $http) {
		//On page load/refresh - get information about directories
		$http.get('http://localhost:9834/api/Explorer/GetDirectories')
						.then(function (response) {
						    $scope.filesizes = response.data.CurrentDirectoryFS;
						    $scope.directories=response.data.SubDirectories;
						    $scope.files = response.data.SubFiles;
						    
						    $scope.currentPath = response.data.CurrentDirectory;
						    $scope.$apply();
						});
				
        
		//On directory click function
		$scope.changeDirectory = function (item, currentPath) {
			//request with parameters
		    $http.get('http://localhost:9834/api/Explorer/GetDirectories', { params: { folder: item, curPath: currentPath } })
				.then(function (response) {
				    				   
				    $scope.directories = response.data.SubDirectories;
				    $scope.files = response.data.SubFiles;
				   
				    $scope.currentPath = response.data.CurrentDirectory;
				    
				    $scope.filesizes[0] = response.data.CurrentDirectoryFS[0];
				    $scope.filesizes[1] = response.data.CurrentDirectoryFS[1];
				    $scope.filesizes[2] = response.data.CurrentDirectoryFS[2];
				    $scope.$apply();
				    
				});
		}
});