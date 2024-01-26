import * as React from 'react';
import Button from '@mui/material/Button';
import Menu from '@mui/material/Menu';
import {  Link, MenuItem } from '@mui/material';
import Logout from './Logout';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { defaultConfig } from './AxiosAuthDefaultHeaders';

export default function MuiUserMenu({isLogged, iconElement}) {
  const [anchorEl, setAnchorEl] = React.useState(null);
  const open = Boolean(anchorEl);
  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };
  const {t} = useTranslation();
  const navigate = useNavigate();
  const handleClose = (nav) => {
    setAnchorEl(null);

  };


  const handleProfile = () =>
  {
    axios.get(process.env.REACT_APP_BASE_URL+"/api/auth/currentId", defaultConfig)
    .then((r) => {navigate("/user/"+r.data.data)})
  }

  return (
<>
      <Button
      classes={{root:"m-0 p-0"}}
        sx={{margin:0,padding:0}}
        id="basic-button"
        aria-controls={open ? 'basic-menu' : undefined}
        aria-haspopup="true"
        aria-expanded={open ? 'true' : undefined}
        onClick={handleClick}
        color="primary"
      >

  {iconElement}     
  

      </Button>
      <Menu classes={{root:"p-0 m-0",paper:"p-0 m-0"}} 
      sx={{margin:0,padding:0}}
        id="basic-menu"
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        MenuListProps={{
          'aria-labelledby': 'basic-button',
        }}
      >
                    {isLogged ? (<><MenuItem onClick={handleClose} sx={{margin:0,padding:0}} ><Logout/></MenuItem>
                    <MenuItem sx={{margin:0,padding:0}} ><Button onClick={handleProfile} variant="text">{t('profile')}</Button></MenuItem></>)

                    :
                    <MenuItem><Link underline='none' href='/login'>{t('loginButton')}</Link></MenuItem>
                    }
                    


      </Menu>
    </>
  );
}