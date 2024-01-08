import { Box, Button, Typography, useMediaQuery, useTheme } from '@mui/material';
import ArrowBackIosIcon from '@mui/icons-material/ArrowBackIos';

export const NotFound = () => {
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  
  return (
    <Box 
      display='flex' 
      flexDirection='column' 
      justifyContent='center' 
      alignItems='center'
      gap={2}
      width='100vw'
      height='100vh'
    >
      <img src="https://c.tenor.com/ftaDAT-sWg4AAAAC/tenor.gif" style={{ width: smDown ? 'inherit' : 'auto' }}/>
      <Box>
        <Typography variant='h1'>404</Typography>
        <Typography variant='body1'>Página não encontrada :/</Typography>
      </Box>
      <Button
        size={smDown ? 'small' : 'large'}
        variant='contained'
        href='javascript:history.back()'
        startIcon={<ArrowBackIosIcon />}
      >
        Voltar para a página anterior
      </Button>
      <Button 
        size={smDown ? 'small' : 'large'}
        href='/inicio'
      >
        Ir para a página inicial
      </Button>
    </Box>
  );
};