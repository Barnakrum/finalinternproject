import { Box, Button, FormControl, Typography, Grid, InputLabel, MenuItem, Select, Stack, TextField, useTheme } from "@mui/material";
import { CollectionTopic } from "../Utilities/CollectionTopic";
import { useTranslation } from "react-i18next";
import ImageUpload from "../Utilities/ImageUpload";
import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import axios from "axios";
import InputFields from "../Utilities/InputFields";
import { defaultConfig } from "../Utilities/AxiosAuthDefaultHeaders";

export default function EditCollection() {
    const navigate = useNavigate();
    const {t} = useTranslation();
    const theme = useTheme();

    const [imageHandle, setImageHandle] = useState("");
    const [name, setName] = useState();
    const [description, setDescription] = useState();
    const [topic, setTopic] = useState();
    const params = useParams();

    const setParentHandle = handle => {
        setImageHandle(handle)
    }


    
    const handleSubmit = (event) => {
        event.preventDefault()
        const data = {
            name: name,
            description: description,
            topic: topic,
            imageHandle: imageHandle,
        }    

        axios.put(process.env.REACT_APP_BASE_URL+"/api/collection/edit/"+params.CollectionId, data,defaultConfig).
        then(r => {
            navigate("/collection/"+params.CollectionId)
        })

    }





    return(<>

        <Grid container justifyContent={"center"} > 

        <form onSubmit={(event) => handleSubmit(event)}>
            <FormControl>
                <Stack direction="column" spacing={3}>
                    <TextField id="name" onChange={(e) => setName(e.target.value)}  required label={t('name')}/>
                    <TextField id="description" onChange={(e) => setDescription(e.target.value)} multiline required label={t('description')}/>

                    <FormControl>
                    <ImageUpload setParentHandle={setParentHandle}/>
                    <Typography variant='caption' color={theme.palette.info.main}>{t('noImageProvided')}</Typography>

                    </FormControl>
                    <FormControl>
                        <InputLabel  id="topic">{t("topic")}</InputLabel>
                        <Select id="topic" required labelId="topic" onChange={(e) => setTopic(e.target.value)}  label={t("topic")}>
                            {CollectionTopic.map((value,index) =>(<MenuItem key={index} value={index}>{t(value)}</MenuItem>))}
                        </Select>
                    </FormControl>
                    <Stack>

                    </Stack>

                    <Stack direction={"row"} justifyContent={"space-around"}>
                    <Button variant="contained" onClick={() =>{navigate("/collection/"+params.CollectionId)}}>{t('back')}</Button>
                    <Button variant="contained" type="submit">{t('submit')}</Button>
                    </Stack>

                </Stack>    
            </FormControl>
        </form>
        </Grid>
    </>)





}