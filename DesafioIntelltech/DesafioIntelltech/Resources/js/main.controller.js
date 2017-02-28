(function () {
	'use strict';

	angular.module('app.MainController', [])
		.controller('MainController', function ($scope, $log, $timeout, $http) {

			var main = this;

			//$log.debug('eteste');

			main.atividade = "";
			main.search = "";
			main.find = function(id) {
				//$log.debug(main.search);
				
				$http.get('api/atividade/' + id).then(function (response) { //if success
					main.atividade = response.data;
				}, function (response) { //else (if failure)

				});
			}

			main.atividades = [];
			$http.get('api/atividade').then(function (response) {
				main.atividades = response.data;
				$log.debug(main.atividades);
			}, function (response) {

			});

			//----------------------------------------

			main.doSecondaryAction = function (event) {
				$mdDialog.show(
				  $mdDialog.alert()
					.title('Ação Secundária')
					.textContent('Ações secundárias podem ser usadas em ações de um clique')
					.ariaLabel('Demo de clique secundário')
					.ok('Bacana!')
					.targetEvent(event)
				);
			};

			main.user = null;
			main.users = null;

			main.loadUsers = function () {

				// Use timeout to simulate a 650ms request.
				return $timeout(function () {

					main.users = main.users || [
					  { id: 1, name: 'Não iniciada' },
					  { id: 2, name: 'Em andamento' },
					  { id: 3, name: 'Concluída' },
					];

				}, 650);
			};
		});
})();