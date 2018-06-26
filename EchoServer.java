import java.net.*;
import java.io.*;

public class EchoServer {
    static int port = 10000;
    
    public static void main(String[] args) {
	try {
	    ServerSocket server = new ServerSocket(port);
	    Socket sock = null;
	    System.out.println("�T�[�o���N�����܂���");
	    while(true) {
		try {
		    sock = server.accept(); // �N���C�A���g����̐ڑ���҂�

		    System.out.println("�N���C�A���g�Ɛڑ����܂���");
		    BufferedReader in = new BufferedReader(
                        new InputStreamReader(sock.getInputStream()));
		    PrintWriter out = new PrintWriter(sock.getOutputStream());
		    String s;
		    while((s = in.readLine()) != null) { // ��s��M
			out.print(s + "\r\n"); // ��s���M
			out.flush();
			System.out.println(s);
		    }
		    sock.close(); // �N���C�A���g����̐ڑ���ؒf
		    System.out.println("�ؒf���܂���");
		} catch (IOException e) {
		    System.err.println(e);
		}
	    }
	} catch (IOException e) {
	    System.err.println(e);
	}
    }
}