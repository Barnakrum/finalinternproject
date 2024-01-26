import { Button, Divider, FormControl, Paper, Stack, TextField, Typography } from "@mui/material";
import { useState } from "react"
import { useTranslation } from "react-i18next";


export default function InputFields({fieldType, setParentFields}) {

    const {t} = useTranslation();



    const [fieldsCount, setFieldsCount] = useState(0);
    const [fieldsArray, setFieldsArray] = useState([]);

    const addField = (e) => {
        fieldsArray.push("")
        setFieldsArray(fieldsArray)
        setFieldsCount(fieldsCount+1)
        

    }
    const removeField = (e) => {
        fieldsArray.splice(fieldsArray.length-1, 1)
        setFieldsArray(fieldsArray)
        setFieldsCount(fieldsCount-1)

    }
    
    const handleNameChange = (e, index) => {
        fieldsArray[index] = e.target.value
        setFieldsArray(fieldsArray)
        setParentFields(fieldsArray)
    }


    return (<>
    <FormControl>
    <Paper elevation={1} sx={{padding:3}} justifyContent="center">
        <Typography>{t(fieldType+"Fields")}</Typography>
        

        {fieldsArray.map((values, index) => (
            <div key={index}>
            <Stack spacing={3} key={index}>

            <TextField required key={index} onChange={(e) => handleNameChange(e,index)}/>
            
            </Stack>
            
            </div>
        ))}
        <Stack direction="row" justifyContent="center">
            <Button variant="text" size="small" onClick={(e) => {addField(e)}}>{t('add')}</Button>
            <Button variant="text" size="small" color="error" onClick={(e) => {removeField(e)}}>{t('remove')}</Button>
        </Stack>
        </Paper>

    </FormControl>
    </>)
}