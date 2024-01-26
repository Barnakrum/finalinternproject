import { AddPhotoAlternate, BorderColor, Delete } from "@mui/icons-material";
import { Button, ButtonGroup } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { defaultConfig } from "../Utilities/AxiosAuthDefaultHeaders";

export default function CollectionManage({collectionId}) {


    const [canManage, setCanManage] = useState(false);
    const navigate = useNavigate();

    const manageDelete = () => {
        axios.delete(process.env.REACT_APP_BASE_URL+"/api/collection/delete/"+collectionId, defaultConfig)
        .then((r) => {navigate("/")})
    }

    useEffect(() => { 
        axios.get(process.env.REACT_APP_BASE_URL+"/api/collection/manage/"+collectionId, defaultConfig)
        .then((r) =>{
            if(typeof(r.data.data)===undefined)
            {

            }
            else
            {
                setCanManage(r.data.data)
            }
            
        })
    

    }, [])
    


    return (
        canManage ?
            <ButtonGroup variant="contained">
                <Button color="success" onClick={()=> {navigate("/item/post/"+collectionId)}} ><AddPhotoAlternate/></Button>
                <Button color="secondary" onClick={()=> {navigate("/collection/edit/"+collectionId)}}><BorderColor/></Button>
                <Button color="error" onClick={() => (manageDelete())}><Delete/></Button>
            </ButtonGroup>
        :
        null

        
    )
}