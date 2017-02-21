(function () {
	'use strict';

	angular.module('app.MainController', [])
		.controller('MainController', function ($scope, $log, $timeout, $http) {

			var main = this;

			//$log.debug('eteste');

			main.atividade = "";
			main.search = "";
			main.find = function(id) {
				$log.debug(main.search);

				$http.get('api/atividade/' + id).then(function (response) { //if success
					main.atividade = response.data;
				}, function (response) { //else (if failure)

				});
			}

			main.atividades = [];
			$http.get('api/atividade').then(function (response) {
				main.atividades = response.data;
			}, function (response) {

			});

			//----------------------------------------

			main.people = [
			  { name: 'Janet Perkins', img: 'img/100-0.jpeg', newMessage: true },
			  { name: 'Mary Johnson', img: 'img/100-1.jpeg', newMessage: false },
			  { name: 'Peter Carlsson', img: 'img/100-2.jpeg', newMessage: false }
			];

			main.doSecondaryAction = function (event) {
				$mdDialog.show(
				  $mdDialog.alert()
					.title('Secondary Action')
					.textContent('Secondary actions can be used for one click actions')
					.ariaLabel('Secondary click demo')
					.ok('Neat!')
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