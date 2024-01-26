import { Menu as MenuIcon } from "@mui/icons-material";
import { AppBar, Toolbar, Grid, Link, Stack, Typography, useTheme, MenuItem, Menu, Box, Button } from "@mui/material";

import * as React from 'react'
import { useTranslation } from 'react-i18next';


export default function NavMenu(props) {



        const navMenuArray = [
        {href:"/", text:"home"},
        {href:"/collection/post", text:"addCollection"},

        ]

        const [anchorEl, setAnchorEl] = React.useState(null);
        const open = Boolean(anchorEl);
        const handleClick = (event) => {setAnchorEl(event.currentTarget);};
        const handleClose = () => {
            setAnchorEl(null);
        };

    const {t} = useTranslation();
    const theme = useTheme();

    return (
        <>
        <AppBar style={{maxWidth:"80vw"}} color="transparent" className="mb-5" position="static">
            <Toolbar>



                <Grid container justifyContent="space-between" alignItems="center" direction="row">
                <Box sx={{display: {xs:"inherit", sm:"none"}}}>
                <Button id="basic-button" aria-controls={open ? 'basic-menu' : undefined} aria-haspopup="true" aria-expanded={open ? 'true' : undefined} onClick={(event) =>{handleClick(event)}} >
                <MenuIcon/>
                </Button>
                <Menu id="basic-menu" anchorEl={anchorEl} open={open} onClose={handleClose} MenuListProps={{ 'aria-labelledby': 'basic-button', }}
                >
                {navMenuArray.map((values,index) => <MenuItem key={index} onClick={handleClose}><Link underline="none" key={index} href={values.href}>{t(values.text)}</Link></MenuItem>)}
                </Menu>
                </Box>
                    <Stack direction="row" sx={{display: {xs:"none", sm:"inherit"}}} spacing={2}>
                        {navMenuArray.map((values,index) => (<Link key={index} underline="none"   href={values.href}><Typography key={index} variant="subtitle2">{t(values.text)}</Typography></Link>))}
                    </Stack>
                    <Stack direction="row" spacing={0}>
                        {props.children}

                    </Stack>
                </Grid>

            </Toolbar>
        </AppBar>
        </>
    )
}