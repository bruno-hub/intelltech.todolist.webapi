(function () {
	'use strict';

	angular.module('app.MainController', [])
		.controller('MainController', function ($scope, $log, $timeout, $http, $mdPanel) {

			var main = this;

			//$log.debug('eteste');

			//------------------------------------ Listar
			main.atividades = [];
			main.todos = todos;
			function todos() {
				$http.get('api/atividade').then(function (response) {
					main.atividades = response.data;
				}, function (response) {

				});
			}

			//------------------------------------ Buscar
			main.search = "";
			main.find = function (id) {
				if (id != "") {
					$http.get('api/atividade/' + id).then(function (response) { //if success
						main.atividades = [];
						main.atividades[0] = response.data;
					}, function (response) { //else (if failure)
						alert("Não encontrado");
					});
				}
			}

			//------------------------------------- Cadastrar
			main.acao = "";

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

			main.cadastrar = function () {
				main.acao = "Cadastro";

				abrirPanel();
			}

			//------------------------------------- Editar
			main.atividade = {};
			main.editar = function (atividade) {
				//$http.get('api/atividade/' + id).then(function (response) { //if success
				main.atividade = atividade;
				main.acao = "Edição";

				abrirPanel();
				//}, function (response) { });
			}

			function PanelDialogCtrl(mdPanelRef) {
				var ctrl = PanelDialogCtrl.prototype;

				ctrl.acao = main.acao;
				ctrl.atividade = main.atividade;

				this._mdPanelRef = mdPanelRef;
			}

			PanelDialogCtrl.prototype.closeDialog = function () {
				var panelRef = this._mdPanelRef;
				
				panelRef && panelRef.close().then(function () {
					angular.element(document.querySelector('.demo-dialog-open-button')).focus();
					panelRef.destroy();
				});

				main.atividade = {};
			}

			PanelDialogCtrl.prototype.salvar = function () {
				var atividade = PanelDialogCtrl.prototype.atividade;

				var panelRef = this._mdPanelRef;
				
				if (main.acao == "Cadastro") {
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
					}, function (response) {

					});
				}

				main.atividade = {};
			}

			//------------------------------------ Excluir
			main.excluir = function (id) {
				$http.delete("/api/atividade/" + id).then(function (response) {
					todos();
				}, function (response) {

				});

			}

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

			//Métodos da primeira execução
			todos();
		});
})();