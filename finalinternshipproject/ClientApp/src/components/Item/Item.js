import { Box, CircularProgress, Paper, Typography } from "@mui/material"
import { Stack } from "@mui/system"
import axios from "axios"
import { useEffect, useState } from "react"
import { useTranslation } from "react-i18next"
import { useParams } from "react-router-dom"
import { defaultConfig } from "../Utilities/AxiosAuthDefaultHeaders"
import ItemManage from "./ItemManage"

export default function Item({isListMode, collectionIdProp, itemIdProp}) {

    const params = useParams()
    const [item, setItem] = useState(null)
    const [itemFieldsNames, setItemFieldsNames] = useState(null)

    const {t} = useTranslation();



        useEffect(() => 
        {
            
        },[])
        
        useEffect(() => {
            try {
                axios.get(process.env.REACT_APP_BASE_URL+"/api/collection/anonadd/"+collectionIdProp, defaultConfig)
                .then((r) => {
                    
                    setItemFieldsNames(r.data.data);
                    
                })
                
                
                
                
            } catch (error) {
                console.log(error)
        }
    }, [])

        useEffect(() => 
        {
            if(isListMode)
            {
                axios.get(process.env.REACT_APP_BASE_URL+"/api/item/get/"+itemIdProp)
                .then((r)=>{setItem(r.data.data);console.log(r.data.data)})
            }
            else
            {
                axios.get(process.env.REACT_APP_BASE_URL+"/api/item/get/"+params.ItemId)
                .then((r)=>{setItem(r.data.data);console.log(r.data.data)})
            }

        },[])
        
        useEffect(() => {
            if(isListMode)
            {
                try {
                    axios.get(process.env.REACT_APP_BASE_URL+"/api/collection/anonadd/"+collectionIdProp, defaultConfig)
                    .then((r) => {    
                        setItemFieldsNames(r.data.data);   
                    })  
                }catch (error) {console.log(error)}
            }
            else
            {
            try {
                axios.get(process.env.REACT_APP_BASE_URL+"/api/collection/anonadd/"+params.CollectionId, defaultConfig)
                .then((r) => {    
                    setItemFieldsNames(r.data.data);   
                })  
            }catch (error) {console.log(error)}
        }
    }, [])

    
    return(
        item === null || itemFieldsNames === null ?
        
        <CircularProgress /> :
        
        <>
    <Paper elevation={3}>
        <Stack direction={"row"} justifyContent="space-between">
            <Typography variant="h4" color={"primary"}>{item.name}</Typography>
            <Box> 
                {isListMode? <ItemManage itemId={itemIdProp} collectionId={collectionIdProp}/>
                :
                <ItemManage itemId={params.ItemId} collectionId={params.CollectionId}/>
                }
                
            </Box>
        </Stack>



        <Stack direction={"row"} spacing={2}>
            <Stack direction="column">
                {itemFieldsNames.booleanFieldsNames.map((value, index) => <Typography key={index}>{value+":"}</Typography>)}
            </Stack>
            <Stack direction="column">
                {item.booleanFields.map((value, index) => <Typography key={index}>{value ==="on" ? t('true') :  t('false')}</Typography>)}
            </Stack>
        </Stack>

        <Stack direction={"row"} spacing={2}>
            <Stack direction="column">
                {itemFieldsNames.dateFieldsNames.map((value, index) => <Typography key={index}>{value+":"}</Typography>)}
            </Stack>
            <Stack direction="column">
                {item.dateFields.map((value, index) => <Typography key={index}>{value}</Typography>)}
            </Stack>
        </Stack>

        <Stack direction={"row"} spacing={2}>
            <Stack direction="column">
                {itemFieldsNames.integerFieldsNames.map((value, index) => <Typography key={index}>{value+":"}</Typography>)}
            </Stack>
            <Stack direction="column">
                {item.integerFields.map((value, index) => <Typography key={index}>{value}</Typography>)}
            </Stack>
        </Stack>

        <Stack direction={"row"} spacing={2}>
            <Stack direction="column">
                {itemFieldsNames.stringFieldsNames.map((value, index) => <Typography key={index}>{value+":"}</Typography>)}
            </Stack>
            <Stack direction="column">
                {item.stringFields.map((value, index) => <Typography key={index}>{value}</Typography>)}
            </Stack>
        </Stack>

        <Stack direction={"row"} spacing={2}>
            <Stack direction="column">
                {itemFieldsNames.multilineFieldsNames.map((value, index) => <Typography key={index}>{value+":"}</Typography>)}
            </Stack>
            <Stack direction="column">
                {item.multilineFields.map((value, index) => <Typography key={index}>{value}</Typography>)}
            </Stack>
        </Stack>

        <Paper elevation={1}>
            <Stack direction="row" justifyContent={"space-between"}>
                <Stack direction="row" spacing={1}>
                    <Typography variant="caption">{t('tags')+": "}</Typography>
                    {item.tags.length>0 ?
                    item.tags.map((tag, index)=><Typography variant="caption" key={index}>{tag}</Typography>)
                    : null}
                </Stack>
                <Stack direction="row" spacing={1}>
                    <Typography variant="caption">{t('likes')+": "}</Typography>
                    <Typography variant="caption">{item.likes}</Typography>
                </Stack>
            </Stack>
        </Paper>




    </Paper>
    </>)
}