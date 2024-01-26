import { SpaceBar } from "@mui/icons-material";
import { Autocomplete, Box, Button, Checkbox, CircularProgress, FormControl, FormControlLabel, InputLabel, List, ListItem, ListItemText, MenuItem, Paper, Select, TextField, Typography } from "@mui/material";
import { Stack } from "@mui/system";
import axios from "axios";
import { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { useNavigate, useParams } from "react-router-dom";
import { defaultConfig } from "../Utilities/AxiosAuthDefaultHeaders";

export default function PostItem({isEdit}) {


    const params = useParams();
    const [isLoading, setIsLoading] = useState(true)
    const [collectionData, setCollectionData] = useState({});
    const {t} = useTranslation();
    const [name, setName] = useState();
    const [tagsArray, setTagsArray] = useState([]);
    const navigate = useNavigate()
    const [tagIndex, setTagIndex] = useState(0)
    const [isButton, setIsButton] = useState(false)

    const [tagsFromDatabase, setTagsFromDatabase] = useState([])


    
    const handleTagsInput = (e) => {
        if(e.code==="Enter")
        {
            let tag = e.target.value
            e.target.value = ""
            tag = tag.split(" ").join("");
            
        if(tag.length>0 && !tagsArray.includes(tag)){
                let tempArray = tagsArray
                tempArray[tagIndex] = tag
                setTagsArray(tempArray)
                setTagIndex(tagIndex+1)
        }
        }
    }

    const handleSubmit = (event) =>{
            
            event.preventDefault();
            const data = {
                name: name,
                tags: tagsArray,
                dateFields: dateFields,
                booleanFields: boolFields,
                integerFields: integerFields,
                multilineFields: multilineFields,
                stringFields: stringFields
            }
            console.log(data)
            if(isEdit)
            {
                axios.put(process.env.REACT_APP_BASE_URL+"/api/item/edit/"+params.ItemId, data, defaultConfig)
                .then((r) => {navigate("/item/"+params.CollectionId+"/"+r.data.data)})
            }
            else
            {
                axios.post(process.env.REACT_APP_BASE_URL+"/api/item/post/"+params.CollectionId, data, defaultConfig)
                .then((r) => {navigate("/item/"+params.CollectionId+"/"+r.data.data)})
            }

    
    }


    
    

    const [boolFields, setBoolFields] = useState([]);
    const handleBoolFieldsChange = (value, index) => {
        let tempArray = boolFields;
        tempArray[index] = value;
        setBoolFields(tempArray);
        console.log(boolFields)
    }

    const [integerFields, setIntegerFields] = useState([]);
    const handleIntegerFieldsChange = (value, index) => {
        let tempArray = integerFields;
        tempArray[index] = value;
        setIntegerFields(tempArray);
        console.log(integerFields)
    }

    const [dateFields, setDateFields] = useState([]);
    const handleDateFieldsChange = (value, index) => {
        let tempArray = dateFields;
        tempArray[index] = value;
        setDateFields(tempArray);
        console.log(dateFields)
    }

    const [stringFields, setStringFields] = useState([]);
    const handleStringFieldsChange = (value, index) => {
        let tempArray = stringFields;
        tempArray[index] = value;
        setStringFields(tempArray);
        console.log(stringFields)
    }

    const [multilineFields, setMultilineFields] = useState([]);
    const handleMultilineFieldsChange = (value, index) => {
        let tempArray = multilineFields;
        tempArray[index] = value;
        setMultilineFields(tempArray);
        console.log(multilineFields)
    }





    useEffect(() => {
      try {
        axios.get(process.env.REACT_APP_BASE_URL+"/api/collection/add/"+params.CollectionId, defaultConfig)
        .then((r) => {
            console.log(r.data.data);
            setCollectionData(r.data.data);
            let newArray = [];
            for(let i=0; i<r.data.data.booleanFieldsNames.length; i++)
            {
                newArray[i] = "true";
            }
            setBoolFields(newArray);
            
            axios.get(process.env.REACT_APP_BASE_URL+"/api/tag/all")
            .then((r) => {
                setTagsFromDatabase(r.data.data)
            })

            setIsLoading(false)})




      } catch (error) {
        console.log(error)
      }
    }, [])
    


    if(isLoading)
    {
        return(<><CircularProgress /></>)
    }
    else
    {
        return(<>
        <Paper elevation={3} >
            <Stack direction="row" justifyContent={"space-between"}>
                <Typography color="primary" variant="h3" >{collectionData.name}</Typography>
                <Typography variant="h3" >{collectionData.usersname}</Typography>
            </Stack>

        <form>
            <FormControl>
                <TextField label={t('name')} variant="outlined" required onChange={(e) => {setName(e.target.value)}}/>

                {/* {collectionData.booleanFieldsNames.map((name, index) => <FormControlLabel key={index} control={<Checkbox defaultValue={false} onLoad={()=>{handleBoolFieldsChange(false, index)}} onChange={(e) => handleBoolFieldsChange(e.target.checked, index)}/>} label={name}/>)} */}
                {collectionData.booleanFieldsNames.map((name, index) => 
                <FormControl>

                 <><InputLabel key={index} id="demo-simple-select-standard-label">{name}</InputLabel>
                  <Select labelId={name} key={(index+1)*2} defaultValue={"true"} onChange={(e) => handleBoolFieldsChange(e.target.value, index)} label={name}>
                    <MenuItem value={"true"} key={index}>{t('true')}</MenuItem>
                    <MenuItem value={"false"} key={(index+1)*2}>{t('false')}</MenuItem>
                  </Select>
                  </>
                </FormControl>
                  )}

                {collectionData.stringFieldsNames.map((name,index) => <TextField onChange={(e) => handleStringFieldsChange(e.target.value, index)} label={name} requried  key={index} variant="outlined"/>)}
                {collectionData.multilineFieldsNames.map((name,index) => <TextField onChange={(e) => handleMultilineFieldsChange(e.target.value, index)} multiline requried  label={name} key={index} variant="outlined"/>)}
                {collectionData.integerFieldsNames.map((name,index) => <TextField onChange={(e) => handleIntegerFieldsChange(e.target.value, index)} type={"number"} requried  label={name} key={index} variant="outlined"/>)}
                {collectionData.dateFieldsNames.map((name,index) =><TextField helperText={name} onChange={(e) => handleDateFieldsChange(e.target.value, index)} type="datetime-local" requried key={index} variant="outlined"/>)}
                <Stack direction="row">
                <Autocomplete disablePortal options={tagsFromDatabase} renderInput={(params) => <TextField {...params} label={t('tags')} helperText={t('noSpaces')} onKeyDown={(e) => {handleTagsInput(e)}}/>}
/>
                    {/* <TextField label={t('tags')} helperText={t('noSpaces')} onKeyDown={(e) => {handleTagsInput(e)}} variant="outlined"/> */}
                    <Stack direction="row" spacing={1}>
                        <Typography>{t('tags')+":"}</Typography>
                        {tagsArray.map((tag,index) => (<Typography variant="caption" key={index}>{tag+""}</Typography>))}
                    </Stack>
                </Stack>
            </FormControl>
            <Stack  direcion={"row"}>
            {isEdit? <Button variant="contained" onClick={() =>{navigate("/item/"+params.CollectionId+"/"+params.ItemId)}}>{t('back')}</Button>: null}
            <Button variant="contained" onClick={(event) =>{handleSubmit(event)}}>{t('submit')}</Button>
            </Stack>




        </form>
        </Paper>

        </>)
    }
}