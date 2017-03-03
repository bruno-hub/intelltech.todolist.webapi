(function () {
	'use strict';

angular.module('app.EmailController', [])
	.controller('EmailController', function ($scope, $log, $timeout, $http, $mdPanel) {
		
		var vm = this;

		//vm.enviar = function () {
		//	$http.post("/api/email/enviar", "").then(function (response) {
		//		alert("E-mail enviado!");
		//	}, function (response) {
		//		alert("Erro: e-mail não foi...");
		//	});
		//}

		function abrirPanel() {
			var position = $mdPanel.newPanelPosition().absolute().center();
			var config = {
				attachTo: angular.element(document.body),
				controller: PanelDialogCtrl,
				controllerAs: 'ctrl',
				disableParentScroll: true,
				templateUrl: 'panelEmail.tmpl.html',
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

		vm.email = {};
		vm.gerenciar = function () {
			$http.get("api/email/1").then(function (response) {
				vm.email = response.data;

				abrirPanel();
			}, function(response){});

		}

		function PanelDialogCtrl(mdPanelRef) {
			var ctrl = PanelDialogCtrl.prototype;

			ctrl.email = vm.email;

			this._mdPanelRef = mdPanelRef;
		}

		PanelDialogCtrl.prototype.closeDialog = function () {
			var panelRef = this._mdPanelRef;
				
			panelRef && panelRef.close().then(function () {
				angular.element(document.querySelector('.demo-dialog-open-button')).focus();
				panelRef.destroy();
			});

			vm.email = {};
		}

		PanelDialogCtrl.prototype.enviar = function () {
			$http.get("api/atividade").then(function (response) {
				vm.email.atividades = response.data;

				$http.post("/api/email/enviar", vm.email).then(function (response) {
					alert("E-mail enviado!");
				}, function (response) {
					alert("Erro: e-mail não foi...");
				});
			}, function (response) { });

			vm.email = {};
		}
	});
})();