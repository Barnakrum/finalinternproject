import { Box, Button, Card, CardActions, CardContent, CardMedia, Divider, Link, Paper, Stack, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";
import ReactMarkdown from "react-markdown";
import { useEffect, useState } from "react";
import CollectionManage from "./CollectionManage";
import axios from "axios";
import Item from "../Item/Item";


export default function Collection({collectionData, variant}) {

    const {t} = useTranslation();
    const [items, setItems] = useState(null);

    useEffect(() => {
      if(variant ==="main")
      {
        axios.get(process.env.REACT_APP_BASE_URL+"/api/item/list/"+collectionData.id)
        .then((r) => {console.log(r.data.data); setItems(r.data.data)})
      }
    }, [])
    
    if(variant === null || variant === "short"){
    return(
    <>
    <Card sx={{ maxWidth: 345 }}>
      <CardMedia
      sx={{maxHeight: 345}}
        component="img"
        src={process.env.REACT_APP_IMAGE_CDN+collectionData.imageHandle}
      />
      <CardContent>
        <Typography gutterBottom variant="h5" component="div" color="primary">
            {collectionData.name}
        </Typography>
        <Typography variant="body2" color="text.secondary">
            {collectionData.topic}
        </Typography>
      </CardContent>
      <Stack direction={"row"} justifyContent="space-between">

      <CardActions>
            <Button size="small" variant="text" element="a" href={"/collection/"+collectionData.id}>{t('visit')}</Button>
      </CardActions>
      <CardActions>
            <CollectionManage collectionId={collectionData.id}/> 
      </CardActions>
      </Stack>
    </Card>
    </>)}
    else if(variant ==="main")
    {
        return(
<>
<Paper elevation={1}>

    <Card sx={{ maxWidth: 800 }}>
            <CardMedia component="img" sx={{maxHeight: 345}} src={process.env.REACT_APP_IMAGE_CDN+collectionData.imageHandle}/>

            <CardContent>
                <Typography gutterBottom variant="h5" component="div" color="primary">
                {collectionData.name}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                {collectionData.topic}
                </Typography>
                <ReactMarkdown>
                {collectionData.description}
                </ReactMarkdown>
            </CardContent>
            <CardActions>
                <CollectionManage collectionId={collectionData.id} />  
            </CardActions>
    </Card>
    {items==null? null :
        <Stack direction={"column"} spacing={1}>
            {items.map((id,index) => <Item key={index} isListMode={true} itemIdProp={id} collectionIdProp={collectionData.id}/>)}
        </Stack>}
        
    </Paper>
</>
        )
    }
}