import { Button, Link, Typography, useTheme } from "@mui/material";
import axios from "axios"
import { useEffect, useState } from "react"
import { useTranslation } from "react-i18next";
import MuiUserMenu from "./MuiUserMenu";
import PersonOutlineIcon from '@mui/icons-material/PersonOutline';
import PersonIcon from '@mui/icons-material/Person';
import { defaultConfig } from "../Utilities/AxiosAuthDefaultHeaders";



export default function DisplayCurrentUser() {


    const theme = useTheme();
    const {t} = useTranslation();
    const [isLogged, setIsLogged] = useState(null);
    
    const [currentUser, setCurrentUser] = useState(null);


    

    useEffect(() => {
        try{

            axios.get(process.env.REACT_APP_BASE_URL+"/api/auth/currentUser", defaultConfig)
            .then((r)=> {
                setCurrentUser(r.data.data);
                setIsLogged(r.data.success);
            })
            }
        catch (error) {
            console.log(error.message)
        }
    }, [])

    
    
    if(isLogged === null)
    {
        return (<Typography variant="caption" color="text.main">{t('loading')}</Typography>)
    }
    else if(isLogged !== null)
    {
        return(
        <>
        <MuiUserMenu iconElement={isLogged ? <PersonIcon /> : <PersonOutlineIcon/>} isLogged={isLogged}/>
        </>)
    }


    
}