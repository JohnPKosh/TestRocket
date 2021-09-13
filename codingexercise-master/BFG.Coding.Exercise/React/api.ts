export const getMedia = (callback: (res) => void) => {
	let xhr = new XMLHttpRequest();
	xhr.open("get", "/api/Media");
	xhr.onreadystatechange = () => {
		if (xhr.readyState === 4) {			
			callback(JSON.parse(xhr.responseText));
		}
	};
	xhr.send();
};

export const submitNewMedia = (mediaRequest: { collection; media; }, callback: (status: number) => void) => {
	let xhr = new XMLHttpRequest();
	xhr.open("post", "/api/media");
	xhr.setRequestHeader("Content-Type", "application/json");
	xhr.onreadystatechange = () => {
		if (xhr.readyState === 4) {
			callback(xhr.status);
		}
	};    
	xhr.send(JSON.stringify(mediaRequest));
};