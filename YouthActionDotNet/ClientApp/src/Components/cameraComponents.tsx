import React from 'react';
import cameraRaw, { destroyCam } from './camera-raw';
const blackscreen  = require("../Assets/black-screen.png");


type Props ={
    setData: (data: string) => void;
}

export class QrReader extends React.Component<Props>{
    state = {
        qrResult:"",
        mounted: false,
        scanner:"",
        loaded:false,
    }

    setQrResult = (result: string) => {
        this.setState({qrResult: result})
        this.props.setData(result);
    }



    componentDidMount = async() =>{
        await this.setState({
            mounted: true
        })
        this.setState({
            scanner: cameraRaw(this.setQrResult, this.state.mounted)
        })
    }

    componentWillUnmount = async() =>{
        await this.setState({
            mounted: false
        })
        destroyCam();
    }

    render(){
        return(
            <div id={"video-container"}>
                
                <video id={"qr-video"} poster={blackscreen} onLoadedData={()=>this.setState({
                    loaded: true,
                })} style={{opacity:(this.state.loaded?1:0)}}></video>
                <div className='cam-options'>
                </div>
            </div>
        )
    }
}