(function () {
	'use strict';

angular.module('app.MainController', [])
	.controller('MainController', function ($scope, $log, $timeout, $http, $mdPanel) {

		var vm = this;

		//$log.debug('eteste');

		//------------------------------------ Listar
		vm.atividades = [];
		vm.todos = todos;
		function todos() {
			$http.get('api/atividade').then(function (response) {
				vm.atividades = response.data;
			}, function (response) {

			});
		}

		//------------------------------------ Buscar
		vm.search = "";
		vm.find = function (id) {
			if (id != "") {
				$http.get('api/atividade/' + id).then(function (response) { //if success
					vm.atividades = [];
					vm.atividades[0] = response.data;
				}, function (response) { //else (if failure)
					alert("Não encontrado");
				});
			}
		}

		//------------------------------------- Cadastrar
		vm.acao = "";

		function abrirPanel() {
			var position = $mdPanel.newPanelPosition().absolute().center();
			var config = {
				attachTo: angular.element(document.body),
				controller: PanelDialogCtrl,
				controllerAs: 'ctrl',
				disableParentScroll: true,
				templateUrl: 'panel.tmpl.html',
				hasBackdrop: true,
				panelClass: 'demo-dialog-example',
				position: position,
				trapFocus: true,
				zIndex: 150,
				clickOutsideToClose: true,
				escapeToClose: true,
				focusOnOpen: true
			};

			$mdPanel.open(config);
		}

		vm.cadastrar = function () {
			vm.acao = "Cadastro";

			abrirPanel();
		}

		//------------------------------------- Editar
		vm.atividade = {};
		vm.editar = function (atividade) {
			//$http.get('api/atividade/' + id).then(function (response) { //if success
			vm.atividade = angular.copy(atividade);
			vm.acao = "Edição";

			abrirPanel();
			//}, function (response) { });
		}

		function PanelDialogCtrl(mdPanelRef) {
			var ctrl = PanelDialogCtrl.prototype;

			ctrl.acao = vm.acao;
			ctrl.atividade = vm.atividade;

			this._mdPanelRef = mdPanelRef;
		}

		PanelDialogCtrl.prototype.closeDialog = function () {
			var panelRef = this._mdPanelRef;
				
			panelRef && panelRef.close().then(function () {
				angular.element(document.querySelector('.demo-dialog-open-button')).focus();
				panelRef.destroy();
			});

			vm.atividade = {};
		}

		PanelDialogCtrl.prototype.salvar = function () {
			var atividade = PanelDialogCtrl.prototype.atividade;
			console.log(atividade.titulo);
			if (atividade.titulo == "") {
				alert("Título inválido");
				return false;
			} else if (atividade.descricao == "") {
				alert("Descrição inválida");
				return false;
			} else {
				var panelRef = this._mdPanelRef;

				if (vm.acao == "Cadastro") {
					$http.post("/api/atividade", atividade).then(function (response) {
						panelRef && panelRef.close().then(function () {
							angular.element(document.querySelector('.demo-dialog-open-button')).focus();
							panelRef.destroy();
						});

						todos();
					}, function (response) { });
				} else {
					$http.put("/api/atividade/" + atividade.id, atividade).then(function (response) {
						panelRef && panelRef.close().then(function () {
							angular.element(document.querySelector('.demo-dialog-open-button')).focus();
							panelRef.destroy();
						});

						todos();
					}, function (response) {

					});
				}

				vm.atividade = {};
			}
		}

		//------------------------------------ Excluir
		vm.excluir = function (id) {
			$http.delete("/api/atividade/" + id).then(function (response) {
				todos();
			}, function (response) {

			});

		}

		//----------------------------------------

		vm.doSecondaryAction = function (event) {
			$mdDialog.show(
				$mdDialog.alert()
				.title('Ação Secundária')
				.textContent('Ações secundárias podem ser usadas em ações de um clique')
				.ariaLabel('Demo de clique secundário')
				.ok('Bacana!')
				.targetEvent(event)
			);
		};

		vm.user = null;
		vm.users = null;

		vm.loadUsers = function () {

			// Use timeout to simulate a 650ms request.
			return $timeout(function () {

				vm.users = vm.users || [
					{ id: 1, name: 'Não iniciada' },
					{ id: 2, name: 'Em andamento' },
					{ id: 3, name: 'Concluída' },
				];

			}, 650);
		};

		//Métodos da primeira execução
		todos();
	});
})();