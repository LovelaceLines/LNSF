import { Box, Divider, Link, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material"
import GitHubIcon from '@mui/icons-material/GitHub';
import InstagramIcon from '@mui/icons-material/Instagram';
import WhatsAppIcon from '@mui/icons-material/WhatsApp';
import logoLar from '../../../assets/LOGO NOVA DO LA.png'
import logoRS from '../../../assets/Logo Rede Saviniana.png'
import lovelace_lines_white from '../../../assets/lovelace_lines_white.svg'
import lovelase_lines_rose from '../../../assets/lovelace_lines_rose.svg'
import { useContext } from "react";
import { ThemeContext } from "../../../Contexts";

export const Footer = () => {
  const theme = useTheme();
  const mdDown = useMediaQuery(theme.breakpoints.down('md'));
  const {themeName} = useContext(ThemeContext);

  return (
    <Box
      component={Toolbar}
      display='flex'
      flexDirection={ mdDown ? 'column' : 'row'}
      alignItems='center'
      paddingY={mdDown ? 2 : 0}
      gap={2}
    >
      <Box
        display='flex'
        flexDirection={mdDown ? 'column' : 'row'}
        alignItems='center'
        gap={theme.spacing(4)}
      >
        <Typography variant='body1'>
          Desenvolvido por Lovelace Lines
        </Typography>

        <Box display='flex' gap={4}>
          <Link href='https://github.com/lovelacelines/' target='_blank' rel='noopener noreferrer' display='flex' alignItems='center'>
            <img src={themeName == 'light' ? lovelase_lines_rose : lovelace_lines_white} height='24px'  />
          </Link>
          <Link color='inherit' href='https://github.com/lovelacelines/' target='_blank' rel='noopener noreferrer' display='flex' alignItems='center'>
            <GitHubIcon />
          </Link>
          <Link color='inherit' href='https://www.instagram.com/lovelacelines/' target='_blank' rel='noopener noreferrer' display='flex' alignItems='center'>
            <InstagramIcon />
          </Link>
          <Link color='inherit' href='https://api.whatsapp.com/send?phone=558893369092' target='_blank' rel='noopener noreferrer' display='flex' alignItems='center'>
            <WhatsAppIcon />
          </Link>
        </Box>

        <Divider orientation={mdDown ? "horizontal" : "vertical"} flexItem />
      </Box>
      <Box
        display='flex'
        marginLeft={mdDown ? '0' : 'auto'}
        gap={4}
      >
        <Link href='https://anbeas.org.br/site/lar-nossa-senhora-de-fatima/' target='_blank' rel='noopener noreferrer' display='flex' alignItems='center'>
          <img style={{ width: '36px' }} src={logoLar} />
        </Link>
        <Link href='https://anbeas.org.br/site/' target='_blank' rel='noopener noreferrer' display='flex' alignItems='center'>
          <img style={{ width: '36px' }} src={logoRS} />
        </Link>
      </Box>
    </Box >
  )
}