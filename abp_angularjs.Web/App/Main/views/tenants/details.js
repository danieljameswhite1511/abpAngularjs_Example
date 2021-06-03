(function () {
	angular.module('app').controller('app.views.tenants.details', [
		'$scope', '$stateParams', 'abp.services.app.tenant',
		function ($scope,  $stateParams ,tenantService) {
			var vm = this;

			var tenantId = $stateParams.id;
			console.log(tenantId);
			vm.tenant = {};

			function init() {
				tenantService.get({
					id: tenantId
			}).then(function (result) {

					
					vm.tenant = result.data;
					
					console.log(vm.tenant);
				});
			}

			init();
		}
	]);
})();