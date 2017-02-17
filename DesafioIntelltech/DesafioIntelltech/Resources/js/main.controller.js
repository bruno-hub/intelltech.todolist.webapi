(function () {
	'use strict';

	angular
	  .module('app.MainController', [])
	  .controller('MainController', function ($scope, $log, $http) {

	  	var vm = this;

	  	$log.debug('eteste');

	  	vm.atividade = "";
	  	vm.find = find;
		vm.search = "";
		function find(id) {
			$log.debug(vm.search);
	  		$http.get('api/atividade/' + id).then(function (response) {
	

	  			vm.atividade = response.data;
	  		}, function (response) {

	  		});
	  	}

	  	vm.atividades = [];
	  	$http.get('api/atividade').then(function (response) {
	  		
	  		vm.atividades = response.data;
	  	}, function (response) {

	  	});

	  });
})();