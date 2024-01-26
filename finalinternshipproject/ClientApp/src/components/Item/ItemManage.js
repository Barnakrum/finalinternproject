import { BorderColor, Delete } from "@mui/icons-material"
import { Button, ButtonGroup } from "@mui/material"
import axios from "axios"
import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { defaultConfig } from "../Utilities/AxiosAuthDefaultHeaders"

export default function ItemManage({itemId,collectionId}) {

    const [canManage, setCanManage] = useState(null)
    const navigate = useNavigate();

    const manageDelete = () => {
        axios.delete(process.env.REACT_APP_BASE_URL+"/api/item/delete/"+itemId, defaultConfig)
        .then((r) => {if(r.data.success){navigate("/collection/"+r.data.data); window.location.reload(true);}})
    }
    useEffect(() => { 
        try {
            axios.get(process.env.REACT_APP_BASE_URL+"/api/item/manage/"+itemId, defaultConfig)
        .then((r) =>{
            if(typeof(r.data.data)===undefined)
            {
                console.log("XXXX")
            }
            else
            {
                setCanManage(r.data.data)
            }
            
        })
        } catch (error) {
            
        }
        
    

    }, [])



    return (
        canManage ?
            <ButtonGroup variant="contained">
                <Button color="secondary" onClick={() => (navigate("/item/edit/"+collectionId+"/"+itemId))}><BorderColor/></Button>
                <Button color="error" onClick={() => (manageDelete())}><Delete/></Button>
            </ButtonGroup>
        :
        null

        
    )
}