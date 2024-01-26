import {Button, TextField, Grid, Stack, Box, Link, Typography, FormControl} from '@mui/material/';
import {useTheme} from '@mui/material/styles'
import { useTranslation } from 'react-i18next';
import { useState } from 'react';
import axios from 'axios';
export default function Register() {

    const theme = useTheme();
    const {t} = useTranslation();
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [response, setResponse] = useState(null);


    const handleSubmit = (event) => {
        // console.table(data)
        event.preventDefault();
        let data = {
            Username: username,
            Password: password
        }


        axios.post(process.env.REACT_APP_BASE_URL+"/api/auth/register", data)
        .then((r) => {setResponse(r.data); console.log(r)})
        .catch((e) => console.log(e))

    }

    return (
        <>
        <Box>
            <Grid container alignItems="center" justifyContent="center">

                <Grid item>
                <form onSubmit={(event) => {handleSubmit(event)}}>

                <FormControl>
                    <Stack spacing={3}>

                    <TextField required={true} id="username" onChange={(e)=>{setUsername(e.target.value)}} label={t('username')} variant="outlined"/>
                    <TextField required={true} type="password" id="password" onChange={(e)=>{setPassword(e.target.value)}} label={t('password')} variant="outlined"/>
                    <>{response===null?(<></>):(response.success===true ?
                     (<Typography variant='caption' color={theme.palette.primary.main}>{t('userCreated')}</Typography>)
                    :(<Typography variant='caption' color={theme.palette.error.main}>{t('userExists')}</Typography>))}</>
                    <Button variant='contained'type='submit' >{t('registerButton')}</Button>
                    </Stack>
                    </FormControl>
                    </form>
                        <Stack className='mt-3'>
                        <Typography variant='caption' color={theme.palette.info.main}>{t('notLogged')}</Typography>
                        <Link href="/login">{t('loginButton')}</Link>
                        </Stack>
                </Grid>

            </Grid>
        </Box>
        </>
    )
}