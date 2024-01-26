import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { defaultConfig } from "./AxiosAuthDefaultHeaders";



export default function PermissionRedirect() {


    const navigate = useNavigate();


    useEffect(() =>{
        axios.get(process.env.REACT_APP_BASE_URL+"/api/auth/currentUser", defaultConfig).
        then((r) => {
            
            if(!r.data.success) navigate("/")
        
        })
    },[])

}
