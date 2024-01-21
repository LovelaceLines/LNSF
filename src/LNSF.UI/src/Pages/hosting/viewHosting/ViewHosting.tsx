
import { useContext, useMemo, useState } from 'react'
import { useEffect } from "react"
import { PeopleContext, RoomContext, iPeopleObject, iRemovePeopleRoom, iRoomObject } from '../../../Contexts';
import { Box, Button, LinearProgress, Pagination, Paper, Table, TableBody, TableCell, TableContainer, TableFooter, TableHead, TableRow, Toolbar, Typography, useMediaQuery, useTheme } from '@mui/material';
import { Environment } from '../../../environment';
import { ButtonAction, SearchButton } from '../../../Component';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useDebounce } from '../../../Component/hooks/UseDebounce';
import PersonIcon from '@mui/icons-material/Person';
import DeleteRoundedIcon from '@mui/icons-material/DeleteRounded';
import AddRoundedIcon from '@mui/icons-material/AddRounded';
import PersonOffRoundedIcon from '@mui/icons-material/PersonOffRounded';
import { Modal, Fade } from '@mui/material';
import InfoOutlinedIcon from '@mui/icons-material/InfoOutlined';

import CloseRoundedIcon from '@mui/icons-material/CloseRounded';
import { HostingContext } from '../../../Contexts/hostingContext';
import { iHostingObject } from '../../../Contexts/hostingContext/type';
import { format, parseISO } from 'date-fns';

export const OtherViewHosting: React.FC = () => {

  const { viewHosting, countHosting } = useContext(HostingContext);
  const [rows, setRows] = useState<iHostingObject[]>([]);
  const [totalCount, setTotalCount] = useState(0);
  const [isLoadind, setIsLoading] = useState(true);
  const [searchParams, setSearchParams] = useSearchParams();
  const { debounce } = useDebounce();
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  const navigate = useNavigate();
  const [modify, setModify] = useState(false);

  const [idModal, setIdModal] = useState(0);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const busca = useMemo(() => {
    return (searchParams.get('busca') || '');
  }, [searchParams])

  const pagina = useMemo(() => {
    return Number(searchParams.get('pagina') || '1');
  }, [searchParams])

  useEffect(() => {
    setIsLoading(true);

    debounce(() => {

      viewHosting(pagina, busca, 'id')
        .then((response) => {
          if (response instanceof Error) {
            setIsLoading(false);
          } else {
            setRows(response);
            setIsLoading(false);
          }
        })
        .catch((error) => {
          setIsLoading(false);
          console.error('Detalhes do erro:', error);
        });
    });

  }, [busca, pagina, modify]);



  useEffect(() => {
    countHosting()
      .then((response) => {
        setTotalCount(response);
      });
  }, []);


  const deletePeopleRoom = (id_: number) => {

    if (confirm('Realmente deseja remover esssa pessoa do quarto?')) {
      console.log(id_)
      // const data: iRemovePeopleRoom = {
      //     peopleId: id_
      // }
      // setIsLoading(true);
      // removePeopleRoom(data)
      //     .then((response) => {
      //         if (response instanceof Error) {
      //             setIsLoading(false);
      //         } else {
      //             setModify(!modify)
      //             setIsLoading(false);
      //         }
      //     })
      //     .catch((error) => {
      //         setIsLoading(false);
      //         console.error('Detalhes do erro:', error);
      //     });
    }
  }

  const addPeopleRoom = (id_: number) => {
    setIdModal(id_)
    setIsModalOpen(true)
  }

  return (
    <Box
      display='flex'
      flexDirection='column'
      width='100%'
    >
      <Box>
        <Toolbar sx={{ margin: 0 }}>
          <Typography
            variant={smDown ? "h5" : "h4"}
            noWrap
            component="div"
            sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
          >
            {!smDown && (<PersonIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
            Pessoas
          </Typography>

          <SearchButton
            textoDaBusca={busca}
            aoMudarTextoDeBusca={texto => setSearchParams({ busca: texto, pagina: '1' }, { replace: true })}
          />

          < ButtonAction
            mostrarBotaoNovo={true}
            mostrarBotaoSalvar={false}
            mostrarBotaoVoltar={false}

            mostrarBotaoApagar={false}
            aoClicarEmNovo={() => { navigate('/inicio/pessoas/gerenciar/cadastrar') }}
          />

        </Toolbar>
      </Box>

      <TableContainer component={Paper} variant='outlined' >
        <Table>
          <TableHead>
            <TableRow>
              <TableCell sx={{ textAlign: 'left' }}>Nome</TableCell>
              <TableCell sx={{ textAlign: 'center' }}>Chegada</TableCell>
              <TableCell sx={{ textAlign: 'center' }}>Saída</TableCell>
              <TableCell sx={{ textAlign: 'center' }}>Ação</TableCell>
            </TableRow>
          </TableHead>

          <TableBody>
            {rows.map(row => (
              <TableRow key={row.id}>

                <TableCell sx={{ textAlign: 'left' }}>
                  {row.patientId}
                </TableCell>

                <TableCell sx={{ textAlign: 'left' }}>
                  {format(parseISO(String(row.checkIn)), 'dd/MM/yyyy')}
                </TableCell>
                {/* <TableCell sx={{ textAlign: 'center' }}>{row.roomId === null ? <PersonOffRoundedIcon /> : getRoomNumber(row.roomId)}</TableCell> */}

                <TableCell sx={{ textAlign: 'left' }}>
                  {format(parseISO(String(row.checkOut)), 'dd/MM/yyyy')}
                </TableCell>

              </TableRow>
            ))}
          </TableBody>

          {totalCount === 0 && !isLoadind && (
            <caption>{Environment.LISTAGEM_VAZIA}</caption>
          )}

          <TableFooter>
            {isLoadind && (
              <TableRow>
                <TableCell colSpan={7}>
                  <LinearProgress />
                </TableCell>
              </TableRow>
            )}

          </TableFooter>
        </Table>
        {(totalCount > 0 && totalCount > Environment.LIMITE_DE_LINHA) && (
          <Box
            display='flex'
            flexDirection='column'
            alignItems='center'
            justifyContent='center'
          >
            <TableRow >
              <TableCell>
                <Pagination
                  page={pagina}
                  count={Math.ceil(totalCount / Environment.LIMITE_DE_LINHA)}
                  color="primary"
                  onChange={(_, newPage) => setSearchParams({ busca, pagina: newPage.toString() })}
                />
              </TableCell>
            </TableRow>
          </Box>
        )}
      </TableContainer>

      <Modal
        open={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        closeAfterTransition
        sx={
          {
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center'
          }
        }
      >
        <Fade in={isModalOpen}>
          <Box sx={{
            minWidth: 300,
            maxWidth: 500,
            bgcolor: 'background.paper',
            p: 2,
            borderRadius: 1.5,
            position: 'relative',
          }}>
            <Box sx={{
              display: 'flex',
              justifyContent: 'end',
              alignItem: 'center',
              position: 'absolute',
              top: 10,
              right: 10,
              zIndex: 1
            }}>
              <Button onClick={() => { setIsModalOpen(false), setModify(!modify) }}>
                < CloseRoundedIcon sx={{ fontSize: '2rem' }} />
              </Button>
            </Box>
            {/* <TelaAddRemovePeopleRoom idPeople={idModal} /> */}
          </Box>
        </Fade>
      </Modal>
    </Box>
  )
}