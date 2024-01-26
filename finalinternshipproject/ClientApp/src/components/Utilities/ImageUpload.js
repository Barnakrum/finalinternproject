import { Avatar, Button, Stack } from "@mui/material";
import { useState } from "react";
import { useTranslation } from "react-i18next";

import { PickerDropPane } from "filestack-react";


export default function ImageUpload({setParentHandle}) {
    const {t} = useTranslation();

    const options = {
        fromSources: ["local_file_system"],
        accept: ["image/*"],
        maxFiles: 1,
        customText: 
        {'Drag and Drop, Copy and Paste Files': t("dragAndDrop"),
        'Uploaded': t('uploaded')}


      }
    const [imageHandle, setImageHandle] = useState(null)  

    const handleUpload = (result) => {
        setParentHandle(result.filesUploaded[0].handle)
        console.log(result.filesUploaded[0].handle)
        setImageHandle(result.filesUploaded[0].handle)
    }

    return (
    <>
    <Stack direction="row" >
        <PickerDropPane className="m-0 p-0" apikey={"Azo0dfsXiRYWfaZ3nuhqzz"} pickerOptions={options} onUploadDone={result => handleUpload(result)}/>
        {imageHandle===null? (<></>) : (<Avatar src={"https://cdn.filestackcontent.com/"+imageHandle} variant="square" sx={{height: 345, width: 345}} alt="User upload preview"/>)}
    </Stack>
    </>
    )   
}