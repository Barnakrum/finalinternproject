import { Button, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";

export default function Logout() {
    const {t} = useTranslation();
    const navigate = useNavigate();

    return(
        <Button onClick={() => {localStorage.removeItem('jwt'); navigate("/authredirect")}} variant="text">
            {t('logout')}
        </Button>
    )
}