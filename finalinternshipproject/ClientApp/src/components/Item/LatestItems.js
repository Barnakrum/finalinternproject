import { Link, Paper, Typography } from "@mui/material"
import { Box, Container, Stack } from "@mui/system"
import axios from "axios"
import { useEffect, useState } from "react"
import { useTranslation } from "react-i18next"

export default function LatestItems({ammount}) {


    const [items, setItems] = useState(null)
    const {t} = useTranslation();

    useEffect(()=>{
        axios.get(process.env.REACT_APP_BASE_URL+"/api/item/latest/"+ammount)
        .then((r) => {setItems(r.data.data)})
    },[])


    return(items===null? null : 
        <Paper elevation={1} square>
            <Container maxWidth="xl" classes={"d-flex flex-content-center"}>

        <Typography variant="h3" color="primary">{t('latestItems')}</Typography>
        <Stack direction="columns" spacing={2} justifyContent="stretch">
        {items.map((item, index) =>
        <Paper key={item.id} elevation={1} square>
            <Typography key={index} variant="h4"><Link key={item.id} underline="none" href={"/item/"+item.collectionId+"/"+item.id}>{item.name}</Link></Typography>





            <Typography key={(index+1)*2}><Link key={item.id} underline="none" href={"/collection/"+item.collectionId}>{t('collection')+": "+item.collectionsName}</Link></Typography>
            <Typography key={(index+1)*3} variant="caption">{t('author')+": "+item.usersName}</Typography>
        </Paper>
        )}
        </Stack>
        </Container>
        </Paper>

        
        )
}