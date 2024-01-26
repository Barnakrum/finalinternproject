import { CssBaseline, ThemeProvider, IconButton, Select, MenuItem, Box, Grid, Paper } from '@mui/material';
import { createTheme } from '@mui/material/styles';
import { Route, Routes } from 'react-router-dom';
import * as React from 'react'
import AppRoutes from './AppRoutes';
import {  useState} from 'react';
import NavMenu from './components/NavMenu';
import { Bedtime, BedtimeOff } from '@mui/icons-material';
import './index.css'
import './i18n.js'
import "../node_modules/flag-icons/css/flag-icons.min.css"
import { useNavigate } from 'react-router-dom';
import useLocalStorage from 'use-local-storage';
import { Stack } from '@mui/system';
import MuiUserMenu from './components/Utilities/MuiUserMenu';
import DisplayCurrentUser from './components/Utilities/DisplayCurrentUser';


    
export default function App()
{

    //const [darkMode, setDarkMode] = useState()
    const [language, selectLanguage] = useState(localStorage.getItem('i18nextLng'))

    const [darkModeStorage, setDarkModeStorage] = useLocalStorage('darkMode', false)

    const navigate = useNavigate();

    const theme = createTheme({
        palette: {
            mode: darkModeStorage? 'dark': 'light',
            primary: {
                main: '#4daeef',
              },
              secondary: {
                main: '#1f54d2',
              },
              error: {
                main: '#f44336',
              },
              success: {
                main: '#4caf50',
                contrastText: '#ffffff'
              },
        }
        
    })

    const changeLanguage = (e) => {
        selectLanguage(e.target.value)
        navigate("?lng="+e.target.value);
        window.location.reload(true);
    }

    const handleDarkModeChange = () => {

        setDarkModeStorage(!darkModeStorage);
       // setDarkMode(!darkMode);
    }

    return (

        <ThemeProvider  theme={theme} >
                
            <div className='App d.flex justify-content-center bg-yellow'>
            <Grid container style={{minWidth:'80vw'}} justifyContent={"center"}>

            
            <CssBaseline/>

        <NavMenu>
            <Stack direction="row" justifyContent="center" alignItems="center">
            <Select id="languageSelect" value={language} onChange={changeLanguage}  inputProps={{sx:{padding: '10px !important'}}} IconComponent={null} sx={{ boxShadow: 'none', '.MuiOutlinedInput-notchedOutline': { border: 0 }}}>
                <MenuItem value="en"><span className="fi fi-gb"></span></MenuItem>
                <MenuItem value="pl"><span className="fi fi-pl"></span></MenuItem>
            </Select>
            <IconButton onClick={() => handleDarkModeChange()} color="primary" size='small'>{darkModeStorage ? <Bedtime/>:<BedtimeOff/> }</IconButton>
            <DisplayCurrentUser/>
            </Stack>
        </NavMenu>


        <Paper elevation={2} square sx={{width:"80%", padding:10}}>


            <Routes>
             {AppRoutes.map((route, index) => {
                 const { element, ...rest } = route;
                 return <Route key={index} {...rest} element={element} />;
                })}
             </Routes> 
                </Paper>
                </Grid>
                </div>
        </ThemeProvider>
    );
  }

  



