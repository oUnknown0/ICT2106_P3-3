    import QrScanner from "qr-scanner";
var  scanner = null;
export default function cameraRaw(setQrResult, destroyHook) {
    
const video = document.getElementById('qr-video');

function setResult(result) {
    console.log(result.data);
    setQrResult(result.data);
}

// ####### Web Cam Scanning #######

scanner = new QrScanner(video, result => setResult(result), {
    onDecodeError: error => {
    },
    highlightScanRegion: true,
    highlightCodeOutline: true,
});

const updateFlashAvailability = () => {
};
scanner.setInversionMode("both");

scanner.start().then(() => {
    updateFlashAvailability();
    // List cameras after the scanner started to avoid listCamera's stream and the scanner's stream being requested
    // at the same time which can result in listCamera's unconstrained stream also being offered to the scanner.
    // Note that we can also start the scanner after listCameras, we just have it this way around in the demo to
    // start the scanner earlier.
});

// for debugging
window.scanner = scanner;


    
return scanner;
}

export function destroyCam(){
    scanner.stop();
    scanner.destroy();
}