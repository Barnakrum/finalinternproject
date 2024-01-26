import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export default function AuthRedirect() {


    const navigate = useNavigate();
    useEffect(() => {
        navigate("/")
        window.location.reload(true);
        
    

    }, [])
    
    return(<></>)
}