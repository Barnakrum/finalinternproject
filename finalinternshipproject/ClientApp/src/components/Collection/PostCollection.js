import { Box, Button, FormControl, Typography, Grid, InputLabel, MenuItem, Select, Stack, TextField, useTheme, Paper } from "@mui/material";
import { CollectionTopic } from "../Utilities/CollectionTopic";
import { useTranslation } from "react-i18next";
import ImageUpload from "../Utilities/ImageUpload";
import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import axios from "axios";
import InputFields from "../Utilities/InputFields";
import { defaultConfig } from "../Utilities/AxiosAuthDefaultHeaders";
import PermissionRedirect from "../Utilities/PermissionRedirect";

export default function PostCollection() {
    const navigate = useNavigate();
    const {t} = useTranslation();
    const theme = useTheme();

    const [imageHandle, setImageHandle] = useState("");
    const [name, setName] = useState();
    const [description, setDescription] = useState();
    const [topic, setTopic] = useState();


    const [stringFieldsNames, setStringFieldsNames] = useState();

    const setParentStringFieldsNames = fieldNames => {
        setStringFieldsNames(fieldNames)
    }

    const [booleanFieldsNames, setBooleanFieldsNames] = useState();

    const setParentBooleanFieldsNames = fieldNames => {
        setBooleanFieldsNames(fieldNames)
    }

    const [integerFieldsNames, setIntegerFieldsNames] = useState();

    const setParentIntegerFieldsNames = fieldNames => {
        setIntegerFieldsNames(fieldNames)
    }

    const [dateFieldsNames, setDateFieldsNames] = useState();

    const setParentDateFieldsNames = fieldNames => {
        setDateFieldsNames(fieldNames)
    }

    const [multilineFieldsNames, setMultilineFieldsNames] = useState();

    const setParentMultilineFieldsNames = fieldNames => {
        setMultilineFieldsNames(fieldNames)
    }








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
            stringFieldsNames: stringFieldsNames,
            booleanFieldsNames: booleanFieldsNames,
            dateTimeFieldsNames: dateFieldsNames,
            multilineFieldsNames: multilineFieldsNames,
            integerFieldsNames: integerFieldsNames
        }

        axios.post(process.env.REACT_APP_BASE_URL+ "/api/collection/post", data, defaultConfig).
            then(r => {
                navigate("/collection/" + r.data.data)
            })

    }





    return (<>
        <Grid container justifyContent={"center"} >
        <PermissionRedirect/>

            <form onSubmit={(event) => handleSubmit(event)}>
                <FormControl>
                    <Stack direction="column" spacing={3}>
                        <TextField id="name" onChange={(e) => setName(e.target.value)} required label={t('name')} />
                        <TextField id="description" onChange={(e) => setDescription(e.target.value)} multiline required label={t('description')} />

                        <FormControl>
                            <ImageUpload setParentHandle={setParentHandle} />
                            <Typography variant='caption' color={theme.palette.info.main}>{t('optionalImage')}</Typography>

                        </FormControl>
                        <FormControl>
                            <InputLabel id="topic">{t("topic")}</InputLabel>
                            <Select id="topic" required labelId="topic" onChange={(e) => setTopic(e.target.value)} label={t("topic")}>
                                {CollectionTopic.map((value, index) => (<MenuItem key={index} value={index}>{t(value)}</MenuItem>))}
                            </Select>
                        </FormControl>
                        <Stack spacing={1}>

                            <InputFields fieldType="string" setParentFields={setParentStringFieldsNames} />


                            <InputFields fieldType="boolean" setParentFields={setParentBooleanFieldsNames} />


                            <InputFields fieldType="multiline" setParentFields={setParentMultilineFieldsNames} />


                            <InputFields fieldType="date" setParentFields={setParentDateFieldsNames} />


                            <InputFields fieldType="integer" setParentFields={setParentIntegerFieldsNames} />



                        </Stack>


                        <Button variant="contained" type="submit">{t('submit')}</Button>


                    </Stack>
                </FormControl>
            </form>
        </Grid>
    </>)





}