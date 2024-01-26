import {Button, TextField, Grid, Stack, Box, Link, Typography, FormControl, FormHelperText, Paper} from '@mui/material/';
import {useTheme} from '@mui/material/styles'
import { useTranslation } from 'react-i18next';
import {useState} from 'react'
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

export default function Login() {

    const {t} = useTranslation();
    const theme = useTheme();
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [response, setResponse] = useState(null);
    const navigate = useNavigate();
    

    const handleSubmit = (event) => {
        // console.table(data)
        event.preventDefault();
        let data = {
            Username: username,
            Password: password
        }


        axios.post(process.env.REACT_APP_BASE_URL+"/api/auth/login", data)
        .then((r) => {
            setResponse(r.data);
            localStorage.setItem('jwt', r.data.data);
            if(r.data.success===true)
            navigate('/authredirect')
            //console.log(localStorage.getItem('jwt'));
            
        })

    }


    return (
        <>

                    <form onSubmit={(event)=>{handleSubmit(event)}}>

                    <FormControl>
                    <Stack spacing={3}>

                    <TextField id="username" required={true} label={t('username')} onChange={(e)=>{setUsername(e.target.value)}} variant="outlined"/>
                    <TextField id="password" type="password" required={true} label={t('password')} onChange={(e)=>{setPassword(e.target.value)}} variant="outlined"/>
                    <>{response === null?(<></>):(response.message.length<1 ? (<></>) : (<Typography variant='caption' color={theme.palette.error.main}>{t("wrongLogin")}</Typography>))}</>
                    <Button variant='contained' type='submit'>{t('loginButton')}</Button>
                    </Stack>
                    </FormControl>
                    </form>

                        <Stack className='mt-3' >
                        <Typography variant='caption' color={theme.palette.info.main}>{t('missingAccount')}</Typography>
                        <Link underline="none" href="/register">{t('registerButton')}</Link>
                        </Stack>

        </>
    )
}