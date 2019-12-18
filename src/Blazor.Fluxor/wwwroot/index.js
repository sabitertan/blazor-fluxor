var tryInitializeFluxor = function () {
	var initializeFluxorScriptElement = document.getElementById("initializeFluxor");
	if (initializeFluxorScriptElement) {
		let script = initializeFluxorScriptElement.innerHTML || "";
		script = script.replace("<!--!-->", "");
		if (script) {
			window.canInitializeFluxor = true;
			eval(script);
		}
		return true;
	}
	return false;
};
