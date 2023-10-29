import { Box, Divider, Paper, Typography, useMediaQuery, useTheme } from "@mui/material"
import GitHubIcon from '@mui/icons-material/GitHub';
import InstagramIcon from '@mui/icons-material/Instagram';
import WhatsAppIcon from '@mui/icons-material/WhatsApp';
import logoLar from '../../../assets/LOGO NOVA DO LA.png'
import logoRS from '../../../assets/Logo Rede Saviniana.png'
export const Footer = () => {

    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const mdDown = useMediaQuery(theme.breakpoints.down('md'));


    return (
        <Box
            display='flex'
            flexDirection={ mdDown ? 'column' : 'row'}
            alignItems='center'
            marginX={smDown ? theme.spacing(2) : mdDown ? theme.spacing(5) : theme.spacing(8)}
            padding={theme.spacing(2)}
            gap={theme.spacing(1)}
            height={mdDown ? '100%' : theme.spacing(9)}
            marginBottom={theme.spacing(3)}
            bgcolor={theme.palette.background.paper}
            component={Paper}

        >
            <Box
                display='flex'
                flexDirection={mdDown ? 'column' : 'row'}
                gap={theme.spacing(4)}
            >
                <Typography>
                    Desenvolvido por Lovelace Lines
                </Typography>

                <Box
                    display='flex'
                    gap={theme.spacing(4)}
                >
                    <image>
                        logo
                    </image>

                    <GitHubIcon />
                    <InstagramIcon />
                    <WhatsAppIcon />
                </Box>

                <Divider orientation={mdDown ? "horizontal" : "vertical"} flexItem />

            </Box>
            <Box
                display='flex'
                marginLeft={mdDown ? '0' : 'auto'}
                gap={theme.spacing(4)}

            >
                <img style={{ width: '40px' }} src={logoLar} />
                <img style={{ width: '40px' }} src={logoRS} />
            </Box>

        </Box >
    )
}