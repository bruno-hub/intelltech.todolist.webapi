(function () {
	'use strict';

angular.module('app.MainController', [])
	.controller('MainController', function ($scope, $log, $timeout, $http, $mdPanel) {

		var vm = this;

		//$log.debug('eteste');

		vm.concluirAtividade = function (atividade) {
			$http.put("/api/atividade/" + atividade.id, atividade).then(function (response) {
				
			}, function (response) { });
		}

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

		function fecharDialogo(panelRef) {
			panelRef && panelRef.close().then(function () {
				angular.element(document.querySelector('.demo-dialog-open-button')).focus();
				panelRef.destroy();
			});

			vm.atividade = {};
		}

		PanelDialogCtrl.prototype.closeDialog = function () {
			var panelRef = this._mdPanelRef;
			
			fecharDialogo(panelRef);
		}

		PanelDialogCtrl.prototype.salvar = function (form) {
			var atividade = PanelDialogCtrl.prototype.atividade;

			if (!form.$valid) {
				return;
			} else {
				atividade.dataHora = new Date();

				//Juntando os objetos de Data (Date) e Hora (Time) num só de DataHora (DateTime)
				atividade.dataHora.setDate(atividade.data.getDate());
				atividade.dataHora.setMonth(atividade.data.getMonth());
				atividade.dataHora.setFullYear(atividade.data.getFullYear());

				atividade.dataHora.setHours(atividade.hora.getHours());
				atividade.dataHora.setMinutes(atividade.hora.getMinutes());
				atividade.dataHora.setSeconds(0);

				var panelRef = this._mdPanelRef;

				if (vm.acao == "Cadastro") {
					$http.post("/api/atividade", atividade).then(function (response) {
						fecharDialogo(panelRef);

						todos();
					}, function (response) {});
				} else if(vm.acao == "Edição"){
					$http.put("/api/atividade/" + atividade.id, atividade).then(function (response) {
						fecharDialogo(panelRef);

						todos();
					}, function (response) {});
				}
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

		//Métodos da primeira execução
		todos();
	});
})();